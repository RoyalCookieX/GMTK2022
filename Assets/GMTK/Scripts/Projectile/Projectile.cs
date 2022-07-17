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
    public static bool RaycastShot(out RaycastHit hit,in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage, in float knockback)
    {
        Vector3 direction = origin - target;
        direction = direction.normalized;

        hit = new RaycastHit();

        if (!Physics.SphereCast(origin, radius, direction, out hit, range, mask)) return false;

        Knockback.TranslateKnockback(hit.collider.gameObject, direction, knockback);
        if (hit.collider.TryGetComponent(out Health health))
        {
            health.ChangeHealth(damage);
        }

        return true;
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

    /// <summary>
    /// Fires a Raycast that explodes at the end.
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    /// <param name="damage">Damage caused by shot</param>
    /// <param name="knockback">Knockback caused by shot</param>
    public static void RocketFire(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage, in float knockback)
    {
        if(RaycastShot(out RaycastHit hit, origin, target, radius, range, mask, damage, knockback))
        {
            ImpactEffects.AoE(hit.transform.position, damage, knockback, radius, mask, false);
            return;
        }
        ImpactEffects.AoE(target, damage, knockback, radius, mask, false);
    }

    /// <summary>
    /// Fires a Raycast that explodes at the end freezing enemies
    /// </summary>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    /// <param name="damage">Damage caused by shot</param>
    /// <param name="knockback">Knockback caused by shot</param>
    public static void Freeze(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage = 0f , in float knockback = 0f)
    {
        if (RaycastShot(out RaycastHit hit, origin, target, radius, range, mask, damage, knockback))
        {
            ImpactEffects.AoE(hit.transform.position, damage, knockback, radius, mask, true);
            return;
        }
        ImpactEffects.AoE(target, damage, knockback, radius, mask, true);
    }


    /// <summary>
    /// Fires a Raycast that teleports the player to position hit
    /// </summary>
    /// <param name="player">Player that will be moved</param>
    /// <param name="origin">Starting position of ray</param>
    /// <param name="target">Target of ray</param>
    /// <param name="radius">Thickness of ray</param>
    /// <param name="range">Max distance of range</param>
    /// <param name="mask">Layer to be targeted</param>
    /// <param name="damage">Damage caused by shot</param>
    /// <param name="knockback">Knockback caused by shot</param>
    public static void TeleportShot(ref GameObject player, in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage = 0f, in float knockback = 0f)
    {
        if (RaycastShot(out RaycastHit hit, origin, target, radius, range, mask, damage, knockback))
        {
            ImpactEffects.Teleport(hit.transform.position, ref player);
            return;
        }
        ImpactEffects.Teleport(target, ref player);
    }

    /// <summary>
    /// Fires a Raycast that spawns object where it hits
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <param name="radius"></param>
    /// <param name="range"></param>
    /// <param name="mask"></param>
    /// <param name="damage"></param>
    /// <param name="knockback"></param>
    public static void InstantiateShot(ref GameObject instance, in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage = 0f, in float knockback = 0f)
    {
        if (RaycastShot(out RaycastHit hit, origin, target, radius, range, mask, damage, knockback))
        {
            ImpactEffects.Spawn(hit.transform.position, ref instance);
            return;
        }
        ImpactEffects.Spawn(target, ref instance);
    }

    public static void Poison(in Vector3 origin, in Vector3 target, in float radius, in float range, in LayerMask mask, in float damage = 0f, in float knockback = 0f)
    {
        if (RaycastShot(out RaycastHit hit, origin, target, radius, range, mask, damage, knockback))
        {
            if(hit.collider.gameObject.TryGetComponent(out Health health))
            {
                health.ChangeHealthOverTime(damage);
            }
        }
    }
}
