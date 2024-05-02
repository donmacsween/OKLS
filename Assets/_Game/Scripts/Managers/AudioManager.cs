// Author      : Don MacSween
// Liscence    : All rights reserved.
// Purpose     : 
// Dependencies: None

// Namespaces
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Properties
    // [Serialised]  [Access]      [Type]       [Name] 
                     public static AudioManager Instance {get; private set;}
    [SerializeField] private       AudioSource  audioSource;
                     public        AudioClip    DefaultMusicLoop;
                     public        AudioClip    LevelMusicLoop;
                     public        AudioClip    winMusicLoop;
                     public        AudioClip    looseMusicLoop;
                     public        AudioClip    panicMusicLoop;
    
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        EnforceSingleton();
        PlayMusic(LevelMusicLoop);
    }
 
    private void EnforceSingleton()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }
    }

    public void LooseMusic()
    {
        PlayMusic(looseMusicLoop);
    }
    public void WinMusic()
    {
        PlayMusic(winMusicLoop);
    }
    public void PanicMusic()
    {
        PlayMusic(panicMusicLoop);
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.loop = true;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
