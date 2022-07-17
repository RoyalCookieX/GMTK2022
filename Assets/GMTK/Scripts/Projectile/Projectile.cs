//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// Fires a raycast that damages first hit
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    /// <param name="damage">Damage caused by shot</param>
    /// <param name="knockback">Knockback caused by shot</param>
    public static void RaycastShot(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage, in float knockback)
    {
        Vector3 direction = origin - target;
        direction = direction.normalized;

        RaycastHit hit;

        if (!Physics.SphereCast(origin, radius, direction, out hit, range, mask)) return;

        Knockback.TranslateKnockback(hit.collider.gameObject, direction, knockback);
        if (hit.collider.TryGetComponent(out Health health))
        {
            health.ChangeHealth(damage);
        }
    }

    /// <summary>
    /// Fires a raycast that goes through everything except walls, damaging targets
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    /// <param name="wallMask">Layer of walls and ground</param>
    /// <param name="damage">Damage caused by shot</param>
    /// <param name="knockback">Knockback caused by shot</param>
    public static void RaycastPierce(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in LayerMask wallMask, in float damage, in float knockback)
    {
        Vector3 direction = origin - target;
        direction = direction.normalized;

        RaycastHit hit;

        if (Physics.SphereCast(origin, radius, direction, out hit, range, wallMask))
        {
            float newRange = hit.distance;
            RaycastPierceWall(origin, target, radius, newRange, mask, damage, knockback);
            return;
        }
        RaycastPierceWall(origin, target, radius, range, mask, damage, knockback);
    }

    /// <summary>
    /// Fires a raycast that goes through everything damaging targets
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    /// <param name="damage">Damage caused by shot</param>
    /// <param name="knockback">Knockback caused by shot</param>
    public static void RaycastPierceWall(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage, in float knockback)
    {
        Vector3 direction = origin - target;
        direction = direction.normalized;

        Ray ray = new Ray(origin, direction);

        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, range, mask);

        foreach(RaycastHit hit in hits)
        {
            Knockback.TranslateKnockback(hit.collider.gameObject, direction, knockback);
            if (hit.collider.TryGetComponent(out Health health))
            {
                health.ChangeHealth(damage);
            }
        }
    }

    public static void RocketFire(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage, in float knockback)
    {

    }
}
