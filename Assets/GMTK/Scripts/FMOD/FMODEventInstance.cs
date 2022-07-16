//Author: Matheus Vilano
//Co-Author: Koda Villela

using UnityEngine;

/// <summary>
/// An FMOD Event Instance.
/// </summary>
public class FMODEventInstance : MonoBehaviour
{
    /// <summary>
    /// The current FMOD Event Instance.
    /// </summary>
    private FMOD.Studio.EventInstance Instance { get; set; }

    /// <summary>
    /// Instantiates a new instance using an instance.
    /// </summary>
    /// <param name="instance">The instance to use to initialize this object.</param>
    public void New(FMOD.Studio.EventInstance instance)
    {
        Instance = instance;
    }

    /// <summary>
    /// Instantiates a new instance using a reference.
    /// </summary>
    /// <param name="reference">The reference to use to create a new instance and initialize this object.</param>
    public void New(FMODUnity.EventReference reference)
    {
        Instance = FMODUnity.RuntimeManager.CreateInstance(reference);
    }

    /// <summary>
    /// Plays the FMOD Event Instance.
    /// </summary>
    public void Play()
    {
        Instance.start();
    }

    /// <summary>
    /// Plays the FMOD Event Instance with a delay.
    /// </summary>
    /// <param name="delayTime">The delay time (in seconds).</param>
    public void PlayDelayed(float delayTime)
    {
        Invoke(nameof(Play), delayTime);
    }

    /// <summary>
    /// Stops the FMOD Event Instance.
    /// </summary>
    /// <param name="stopMode">How to stop the sound: allow fade out VS stop immediately.</param>
    public void Stop(FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        Instance.stop(stopMode);
        Instance.release();
    }

    /// <summary>
    /// Gets a local parameter by name.
    /// </summary>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="paramValue">The current value of the paramter (out).</param>
    public void GetParameter(string paramName, out float paramValue) => Instance.getParameterByName(paramName, out paramValue);

    /// <summary>
    /// Sets a local parameter by name.
    /// </summary>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="paramValue">The new value of the paramter.</param>
    public void SetParameter(string paramName, float paramValue) => Instance.setParameterByName(paramName, paramValue);
}
