using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
                         
public class CubeController : MonoBehaviour, IPoolable
{
    public Transform nextCubePoint;
    [SerializeField] private Rigidbody rg;
    public bool IsCombined = false;
    public UnityAction<GameObject> OnCollide;
    public static UnityAction DecreaseTower ;
    private void OnCollisionEnter(Collision collision)
    {
        if (!IsCombined && !collision.gameObject.CompareTag("Ground"))
        {
            IsCombined = true;
            OnCollide?.Invoke(this.gameObject);
        }
        else if (IsCombined && collision.gameObject.CompareTag("Hurdle")) {
            IsCombined = false;
            this.transform.parent = null;
            DecreaseTower();
          //  LeanPool.Despawn(gameObject, 2f);
        }
    }

    public void Reset()
    {
       
    }
    void IPoolable.OnDespawn()
    {
        IsCombined = false;
    }

    void IPoolable.OnSpawn()
    {
  
    }
}
