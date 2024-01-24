using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //Music
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip   musicClip;
    [SerializeField] private AudioClip   musicLoop;
                     private bool        isLooping = false;
    //

    private void Awake()
    {
        if (musicSource == null) 
        {
            musicSource = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        musicSource.clip = musicClip;
        musicSource.loop = false;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLooping)
        {
            if(!musicSource.isPlaying) 
            {
                musicSource.clip = musicLoop;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }
}
