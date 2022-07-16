//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AcidPool : MonoBehaviour
{
    BoxCollider _boxCollider;
    [SerializeField] float _damage = .1f;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (TryGetComponent(out Health health))
        {
            health.ChangeHealth(_damage);
        }
    }
}
