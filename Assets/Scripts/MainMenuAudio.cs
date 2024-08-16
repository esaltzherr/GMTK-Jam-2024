using UnityEngine;
using UnityEngine.Audio;

public class MainMenuAudio: MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic; // Assign your music clip in the inspector
    // public AudioMixer audioMixer; // Assign your AudioMixer in the inspector
    // public string exposedVolumeParameter = "MusicVolume"; // The exposed parameter name in the AudioMixer

    

    void Start()
    {
        // Add an AudioSource component if not already attached
        // audioSource = gameObject.AddComponent<AudioSource>();

        // Set the audio clip to the audio source
        audioSource.clip = menuMusic;

        // Loop the music
        audioSource.loop = true;

        // Play the music
        audioSource.Play();

    }

   
}
