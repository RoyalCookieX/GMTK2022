//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffects : MonoBehaviour
{
    /// <summary>
    /// Creates Overlap sphere at desired location, damaging enemies
    /// </summary>
    /// <param name="position">Location where overlap shpere will spawn</param>
    /// <param name="damage">Damage of explosion</param>
    /// <param name="force">Force of knockback</param>
    /// <param name="radius">Radius of overlap shpere</param>
    /// <param name="target">Layer mask targeted</param>
    /// <param name="doesFreeze">Freezes instead of dealing damage and knockback</param>
    public void AoE(in Vector3 position, in float damage, in float force, in float radius, in LayerMask target, in bool doesFreeze)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, radius, target);
        foreach (var other in hitColliders)
        {
            if (doesFreeze)
            {
                //TODO : Freeze Enemy Movement
                return;
            }

            Knockback.ExplosionKnockback(gameObject, position, force, radius);
            if (other.TryGetComponent(out Health health))
            {
                health.ChangeHealth(damage);
            }
        }
    }

    /// <summary>
    /// Teleports Target to location
    /// </summary>
    /// <param name="position">Location to be teleported to</param>
    /// <param name="target">Teleported Object</param>
    public void Teleport(in Vector3 position, ref GameObject target)
    {
        target.transform.position = position;
    }

    /// <summary>
    /// Spawns Projectiles
    /// </summary>
    /// <param name="position">Location where prjectiles will spawn</param>
    /// <param name="projectile">Projectile to spawn</param>
    /// <param name="quantity">Number of projectiles to be spawned</param>
    public void SpawnProjectile(in Vector3 position, ref GameObject projectile, in int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            //TODO: Instantiate more projectiles. Needs Pool System
        }
    }
}
