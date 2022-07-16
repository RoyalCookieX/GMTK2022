//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Laser : MonoBehaviour
{
    private RaycastHit[] _hits = new RaycastHit[64];

    /// <summary>
    /// Fires a raycast that damages first hit
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    public void LaserFire(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask)
    {
        Vector3 direction = origin - target;
        direction = direction.normalized;
        RaycastHit hit;
        Physics.SphereCast(origin, radius, direction, out hit, range, mask);

        //TODO: Try get component and add damage
    }

    /// <summary>
    /// Fires a raycast that goes through everything damageing targets
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    public void PierceFire(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask)
    {
        Vector3 direction = origin - target;
        direction = direction.normalized;
        Ray ray = new Ray(origin, direction);
        Physics.SphereCastNonAlloc(ray, radius, _hits, range, mask);

        //TODO: Try get component and add damage

        Array.Clear(_hits, 0, _hits.Length);
    }
}
