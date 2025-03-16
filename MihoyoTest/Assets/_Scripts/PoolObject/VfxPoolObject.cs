using Core;
using UnityEngine;


public class VfxPoolObject : MonoBehaviour,IPoolObject
{
    [SerializeField] private ParticleSystem particle;

    public void OnActivate()
    {
        particle.Play();
    }

    public void OnDeactivate()
    {
        particle.Stop();
    }
    
    private void OnParticleSystemStopped()
    {
        ObjectPool.Instance.ReturnObject(this.gameObject.name, this.gameObject);
    }
}
