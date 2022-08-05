using UnityEngine;

public class ArmyStateEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ringParticle;
    private ParticleSystem.MainModule main;

    private void Start()
    {
        main = _ringParticle.main;
        ChangeEffectColor(Color.blue);
    }

    public void ChangeEffectColor(Color color)
    {
        main.startColor = color;
    }
}
