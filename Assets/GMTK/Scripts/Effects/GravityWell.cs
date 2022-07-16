//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GravityWell : MonoBehaviour
{
    [SerializeField] LayerMask _targetableMask;
    [SerializeField] float _pullSpeed = 3f;
    [SerializeField] float _radius = 5f;
    Vector3 _targetPosition;
    SphereCollider _sphereCollider;

    private void Awake()
    {
        _targetPosition = transform.position;
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _radius;
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != _targetableMask) return;
        Vector3 currentPosition = other.transform.position;
        _targetPosition = new Vector3(_targetPosition.x, currentPosition.y, _targetPosition.z);
        other.gameObject.transform.position = Vector3.MoveTowards(currentPosition, _targetPosition, _pullSpeed * Time.deltaTime);
    }
}
