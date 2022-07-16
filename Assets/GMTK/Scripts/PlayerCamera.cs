using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float Pitch { get; private set; }

    [Header("Components")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [Header("Properties")]
    [SerializeField] private Vector2 _pitchRange = new Vector2(-70f, 70f);
    [SerializeField] private bool _invert = true;

    private CinemachinePOV _pov;

    public void AddPitch(float pitch)
    {
        float invert = _invert ? -1f : 1f;
        SetPitch(Pitch + invert * (pitch * Time.deltaTime));
    }

    private void SetPitch(float pitch)
    {
        Pitch = Mathf.Clamp(pitch, _pitchRange.x, _pitchRange.y);
        _pov.m_VerticalAxis.Value = Pitch;
    }

    private void Start()
    {
        _pov = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        SetPitch(_pov.m_VerticalAxis.Value);
    }
}