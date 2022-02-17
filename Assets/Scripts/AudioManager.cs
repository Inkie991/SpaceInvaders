using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    private AudioClip mainMusic;
    private AudioClip enemyDeath;
    private AudioSource musicSource;
    private AudioSource audioSource;
    public ManagerStatus Status { get; private set; }
    
    public void Startup()
    { 
        Debug.Log("Audio manager starting...");
        audioSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        mainMusic = Resources.Load<AudioClip>("Audio/mainMusic");
        enemyDeath = Resources.Load<AudioClip>("Audio/enemyDeath");
        musicSource.clip = mainMusic;
        musicSource.loop = true;
        musicSource.Play();
        Status = ManagerStatus.Started;
    }
    
    // Plays a sound when an enemy dies
    public void DeathSound()
    {
        audioSource.PlayOneShot(enemyDeath);
    }

    // Handles "OnClick" event and calls MuteSound function
    public void OnSoundClick()
    {
        // If you assign onClick at UI - it clones this class without Startup method call
        Managers.Audio.MuteSound();
    }

    // Mutes/Unmutes sound
    private void MuteSound()
    {
        musicSource.volume = musicSource.volume == 1f ? 0f : 1f;
        audioSource.volume = audioSource.volume == 1f ? 0f : 1f;
    }
}
