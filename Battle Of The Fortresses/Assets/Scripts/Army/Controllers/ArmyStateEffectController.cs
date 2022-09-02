using UnityEngine;
using System;

public class ArmyStateEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ringParticle;
    private ParticleSystem.MainModule main;
    private int countOfUnitsInside = 0;

    private void Start()
    {
        ArmyCommander.OnCountOfUnitsChange += ChangeCountOfUnits;
        main = _ringParticle.main;
        ChangeEffectColor(Color.blue);
    }

    public void ChangeEffectColor(Color color)
    {
        main.startColor = color;
    }

    public void ChangeCountOfUnits(int newCount)
    {
        countOfUnitsInside = newCount;
        float newScale = (Mathf.Sqrt(countOfUnitsInside)) / 2;
        transform.localScale = new Vector3(newScale, newScale, newScale);
        Debug.Log("Current count units inside ring: " + countOfUnitsInside);
    }
}
