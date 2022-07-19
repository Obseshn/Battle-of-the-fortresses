using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyFormator
{

    [SerializeField] private float _spread = 2f;
    [SerializeField] private int _unitWidth = 3;
    [SerializeField] private int _unitDepth = 3;
    [SerializeField] private bool _isHollow = true;
   
    private List<Vector3> _points = new List<Vector3>();

    /// <summary>
    /// Method that line up units.
    /// </summary>
    /// <param name="unitsToFormate"></param>
    /// <param name="destination">Around this point army will be line up</param>
    /// <param name="moveSpeed"></param>
    public void SetFormation(List<GameObject> unitsToFormate, Transform destination, float moveSpeed)
    {
        _points = EvaluatePoints().ToList();

        for (int i = 0; i < unitsToFormate.Count; i++)
        {
            unitsToFormate[i].transform.position = Vector3.MoveTowards(unitsToFormate[i].transform.position,
               destination.position + _points[i], moveSpeed * Time.deltaTime);
        }
    }

    public IEnumerable<Vector3> EvaluatePoints()
    {
        var middleOffset = new Vector3(_unitWidth * 0.5f, 0, _unitDepth * 0.5f);

        for (int x = 0; x < _unitWidth; x++)
        {
            for (int z = 0; z < _unitDepth; z++)
            {
                if (_isHollow && x != 0 && x != _unitWidth - 1 && z != 0 && z != _unitDepth - 1)
                    continue;

                var position = new Vector3(x, 0, z);

                position -= middleOffset;

                position *= _spread;

                yield return position;
            }
        }
    }
}
