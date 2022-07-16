//Author: Matheus Vilano
//Co-Author: Koda Villela

using UnityEngine;

/// <summary>
/// An FMOD Event Reference.
/// </summary>
[CreateAssetMenu(fileName = "FMOD_Event", menuName = "FMOD/Event Reference", order = 1)]
public class FMODEventReference : ScriptableObject
{
    /// <summary>
    /// The FMOD event reference to be used to play/stop/manipulate sounds.
    /// </summary>
    [SerializeField, Tooltip("The FMOD event reference to be used to play/stop/manipulate sounds.")] private FMODUnity.EventReference reference;

#region OneShots

    /// <summary>
    /// Fire-and-forget. The sound will be played without a position (2D) and released immediately after it's done playing.
    /// </summary>
    public void PlayOneShot() => FMODUnity.RuntimeManager.PlayOneShot(reference);

    /// <summary>
    /// Fire-and-forget. The sound will be played at the specified position (3D) and released immediately after it's done playing.
    /// </summary>
    /// <param name="position">Where to play the sound.</param>
    public void PlayOneShotAtLocation(Vector3 position) => FMODUnity.RuntimeManager.PlayOneShot(reference, position);

    /// <summary>
    /// Override for events that use game objects.
    /// </summary>
    /// <param name="gameObject">Object to get the location.</param>
    public void PlayOneShotAtLocation(GameObject gameObject) => FMODUnity.RuntimeManager.PlayOneShot(reference, gameObject.transform.position);

    /// <summary>
    /// Fire-and-forget. The sound will be played attached to the specified game object (3D) and released immediately after it's done playing.
    /// </summary>
    /// <param name="gameObject">The game object to attach the sound to.</param>
    public void PlayOneShotAttached(GameObject gameObject) => FMODUnity.RuntimeManager.PlayOneShotAttached(reference.Guid, gameObject);
#endregion

#region Instances

    /// <summary>
    /// Adds a new FMOD Event Instance to the specified game object and plays its sound(s).
    /// </summary>
    /// <param name="gameObject">The game object to add a new FMOD Event Instance to.</param>
    /// <remarks>FMOD Event Instances are good when dealing with loops.</remarks>
    public void PlayInstance(GameObject gameObject)
    {
        FMODEventInstance emitter;
        
        if (gameObject.TryGetComponent(out emitter))
        {
            emitter.Play();
        }
        else
        {
            emitter = gameObject.AddComponent<FMODEventInstance>();
            emitter.New(reference);
            emitter.Play();
        }
    }

    /// <summary>
    /// Removes the first FMOD Event Instance to the specified game object and stops its sound(s).
    /// </summary>
    /// <param name="gameObject">The game object to remove the FMOD Event Instance from.</param>
    /// <remarks>FMOD Event Instances are good when dealing with loops.</remarks>
    public void StopInstance(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out FMODEventInstance emitter))
        {
            emitter.Stop();
        }
    }

    /// <summary>
    /// Removes the first FMOD Event Instance to the specified game object and stops its sound(s). It also destroys the Event Instance.
    /// </summary>
    /// <param name="gameObject">The game object to remove the FMOD Event Instance from.</param>
    /// <remarks>FMOD Event Instances are good when dealing with loops.</remarks>
    public void StopAndDestroyInstance(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out FMODEventInstance emitter))
        {
            emitter.Stop();
            Destroy(emitter);
        }
    }

#endregion

#region Globals

    /// <summary>
    /// Gets a global parameter by name.
    /// </summary>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="paramValue">The current value of the parameter (out).</param>
    public static void GetGlobalParameter(string paramName, out float paramValue) => FMODUnity.RuntimeManager.StudioSystem.getParameterByName(paramName, out paramValue);

    /// <summary>
    /// Sets a global parameter by name.
    /// </summary>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="paramValue">The new value of the parameter.</param>
    public static void SetGlobalParameter(string paramName, float paramValue) => FMODUnity.RuntimeManager.StudioSystem.setParameterByName(paramName, paramValue);

#endregion
}
