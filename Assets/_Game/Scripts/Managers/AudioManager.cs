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
    
    // Unity Methods
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EnforceSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    // Public Methods 

    // Private Methods   
    private void EnforceSingleton()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }
    }
}
