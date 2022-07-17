using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolHandler
{
    public LinkedPool<PooledObject> Pool { get; private set; }

    private PooledObject _prefab;
    private Vector3 _hidePosition = new Vector3(0f, -1000f, 0f);

    public PoolHandler(PooledObject prefab, int poolSize)
    {
        _prefab = prefab;
        Pool = new LinkedPool<PooledObject>(OnCreateItem, OnTakenItem, OnReturnItem, OnDestroyItem, true, poolSize);
        Debug.Log($"{_prefab.name} pool initialized with size {poolSize}");
    }

    // called when pool is empty and we need a new object
    private PooledObject OnCreateItem()
    {
        PooledObject instantiated = GameObject.Instantiate(_prefab, _hidePosition, Quaternion.identity);
        instantiated.Pool = Pool;

        return instantiated;
    }

    // called when object is removed from the pool for use
    private void OnTakenItem(PooledObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    // called when object is returned to the pool to wait for next use
    private void OnReturnItem(PooledObject obj)
    {
        obj.transform.position = _hidePosition;
        obj.gameObject.SetActive(false);
    }

    // called when object is destroyed (permanently removed from pool) (usually when too many overflow objects are created)
    private void OnDestroyItem(PooledObject obj)
    {
        GameObject.Destroy(obj.gameObject);
    }
}
