using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ArmyCommander : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed;

    [SerializeField] private ArmyFormator armyFormator /*= new ArmyFormator()*/;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();

    private Transform _parent;

    private void Awake()
    {
        _parent = new GameObject("Unit Parent").transform;
    }

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            SpawnUnit();
        }
        
    }
    private void Update()
    {
        armyFormator.SetFormation(_spawnedUnits, transform, _unitSpeed);
    }

    public void SpawnUnit(/* IEnumerable<Vector3> points*/ )
    {
            var unit = Instantiate(_unitPrefab, transform.position /*+ pos*/, Quaternion.identity, _parent);
            _spawnedUnits.Add(unit);
    }

    public void KillUnit(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
}
