using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] // Makes the class visible in the inspector
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] // Hides in inspector
    public AudioSource source;
}
