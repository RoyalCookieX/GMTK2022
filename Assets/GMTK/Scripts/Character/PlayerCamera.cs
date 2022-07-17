using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public void SetYawPitch(Vector2 yawPitch)
    {
        Quaternion lookRotation = Quaternion.Euler(yawPitch.y, yawPitch.x, 0f);
        transform.rotation = lookRotation;
    }
}