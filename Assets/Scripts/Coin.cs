using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;
using System;

public class Coin : MonoBehaviour
{
    public static event Action AddScore;
    void Start()
    {
        // Rotate Game Object on Y axis in loops
        transform.DORotate(new Vector3(0,20,0),0.1f,RotateMode.Fast)
            .SetLoops(-1,LoopType.Incremental);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddScore?.Invoke();
        LeanPool.Despawn(gameObject);
    }

}
