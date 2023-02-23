
using UnityEngine;

public class OptimiseParticle : MonoBehaviour
{
    public ParticleSystem targetParticleSystem;

    private void Update()
    {
        if (!targetParticleSystem.IsAlive())
            Destroy(targetParticleSystem.gameObject);
    }
}
