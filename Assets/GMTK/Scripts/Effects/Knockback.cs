//Made by Koda Villela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    /// <summary>
    /// Creates a Exxplosion force
    /// </summary>
    /// <param name="target">Target affected</param>
    /// <param name="position">Position it creates</param>
    /// <param name="force">Strenght of knockback</param>
    /// <param name="radius">Radious of explosion</param>
    /// <param name="upwardsForce">Upwards modifier</param>
    /// <param name="mode">Force mode</param>
    public static void ExplosionKnockback(in GameObject target, in Vector3 position, in float force, in float radius, in float upwardsForce = 0f, in ForceMode mode = ForceMode.Force)
    {
        if (target is null) return;

        if (target.TryGetComponent(out Rigidbody rb))
        {
            rb.AddExplosionForce(force, position, radius, upwardsForce, mode);
        }
    }

    /// <summary>
    /// Translates object
    /// </summary>
    /// <param name="target">Object to translate</param>
    /// <param name="direction">Direction to translate</param>
    /// <param name="force">Force of Translation</param>
    public static void TranslateKnockback(in GameObject target, in Vector3 direction, in float force)
    {
        if (target is null) return;

        target.transform.Translate(direction * force);
    }
}
