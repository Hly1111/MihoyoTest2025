using UnityEngine;
using Core;

public class Projectile : MonoBehaviour,IPoolObject
{
    [SerializeField] private ParticleSystem projectileVFX;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private EnemyAIController _source;
    
    public void SetSource(EnemyAIController source)
    {
        _source = source;
        rb.isKinematic = true;
    }
    
    public EnemyAIController GetSource()
    {
        return _source;
    }
    
    public void Launch(Vector3 direction)
    {
        projectileVFX.Play();
        rb.isKinematic = false;
        rb.linearVelocity = direction.normalized * speed;
        Invoke(nameof(ReturnToPool), 2f);
    }
    
    public void OnActivate()
    {
        projectileVFX.Stop();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        _source = null;
    }

    public void OnDeactivate()
    {
        projectileVFX.Stop();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        _source = null;
    }
    
    private void ReturnToPool()
    {
        ObjectPool.Instance.ReturnObject(this.gameObject.name, this.gameObject);
    }
}
