//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AcidPool : PooledObject
{
    public Team Team { get; set; }

    BoxCollider _boxCollider;
    [SerializeField] float _damage = .1f;
    [SerializeField] float _life = 2f;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;

        StartCoroutine(DestroySelf());

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CharacterHealth health))
        {
            health.Damage(Team, _damage);
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(_life);
        Pool.Release(this);
    }


}
