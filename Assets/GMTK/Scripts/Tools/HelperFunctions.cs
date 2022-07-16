using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    /// <summary>
    /// Compares int values as binary using & to see if there's a layer in common
    /// </summary>
    /// <param name="mask">Layer Mask that includes</param>
    /// <param name="layer">Layer that might be included</param>
    /// <returns>If the layer is included as a bool</returns>
    public static bool Includes(
          this LayerMask mask,
          int layer)
    {
        return (mask.value & 1 << layer) > 0;
    }
}
