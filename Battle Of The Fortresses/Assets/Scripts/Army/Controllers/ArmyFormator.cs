using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class ArmyFormator : MonoBehaviour
{
    [SerializeField] private float _spread = 2f;
/*    [SerializeField] private int _unitWidth = 3;
    [SerializeField] private int _unitDepth = 3;*/
    [SerializeField] private bool _isHollow = true;
    [SerializeField] private float _noise = 0f;
    [SerializeField] private float currentLength;
    
   
    private List<Vector3> points = new List<Vector3>();

    /// <summary>
    /// Method that line up units.
    /// </summary>
    /// <param name="unitsToFormate"></param>
    /// <param name="destination">Around this point army will be line up</param>
    /// <param name="moveSpeed"></param>
    public void SetFormation(List<ArmyUnit> unitsToFormate, Transform destination, float moveSpeed)
    {
        points = EvaluatePoints(unitsToFormate.Count, currentLength).ToList();

        for (int i = 0; i < unitsToFormate.Count; i++)
        {
            unitsToFormate[i].transform.position = Vector3.MoveTowards(unitsToFormate[i].transform.position,
               destination.position + points[i], moveSpeed * Time.deltaTime);
        }

    }

    public IEnumerable<Vector3> EvaluatePoints(int countOfUnits, float lengthOfSide)
    {
        var middleOffset = new Vector3(lengthOfSide * 0.5f, 0, lengthOfSide * 0.5f);

        for (int z = 0; z < lengthOfSide; z++)
        {
            for (int x = 0; x < lengthOfSide; x++)
            {
                if (_isHollow && x != 0 && x != lengthOfSide - 1 && z != 0 && z != lengthOfSide - 1)
                    continue;

                var position = new Vector3(x, 0, z);

                position -= middleOffset;

               /* position += GetNoise(position);*/

                position *= _spread;

                yield return position;
            }
        }
    }

   /* private Vector3 GetNoise(Vector3 pos)
    {
        var noise = Mathf.PerlinNoise(pos.x * _noise, pos.z * _noise);

        return new Vector3(noise, 0, noise);
    }*/

    public void SetLenghtOfSide(float countOfUnits)
    {
        currentLength = (float)Math.Ceiling(Mathf.Sqrt(countOfUnits));
        Debug.Log(currentLength + " - curLen");
    }
}
