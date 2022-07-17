using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class PooledObject : MonoBehaviour
{
    [Header("Pooled Object")]
    [SerializeField] private int _poolSize = 10;

    public int PoolSize => _poolSize;
    // new type added in UnityEngine.Pool, a special collection type used for object pooling
    public LinkedPool<PooledObject> Pool { get; set; }
}

