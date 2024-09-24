using UnityEngine.Audio;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

[System.Serializable] // to add for every sound audioSorce to avois sound cnflicts
public class SoundClips
{
    public AudioClip clip;
    public string clipName;

    [Space(10)]
    [Range(0f, 1f)]
    public float Volume = 1;
    // public bool PlayOnAwake;
    //public bool loopClip;
    /*[Range(0.1f,3f)] public float pitch =1;  // not needed now future plans*/
    public bool looping;

    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class ClipsRandom
{
    public AudioClip[] clips;
    public string clipName;

    [Space(10)]
    [Range(0f, 1f)]
    public float Volume = 1;
    [HideInInspector]
    public AudioSource audioSource;
}

public class AudioManagerMain : MonoBehaviour
{
    public static AudioManagerMain instance = null; // singletone pattern

    public bool musicMuted = false;
    // public AudioListener audioListener;
    public AudioClip music;

    [Header("Sound Clips Single")]
    public SoundClips[] soundClips;
    public SoundClips[] musicClips;


    [Header("Randomized Clips")]
    public ClipsRandom[] clipsRandom;

    [Header("Audio Sources")]
    public AudioSource effectsSource;
    public AudioSource musicSource; // for background music

    [Header("Pitch Settings")]
    public float lowPitchRange = .95f;   // for randomizing
    public float highPitchRange = 1.05f;

    [Header("Mixers")]
    public AudioMixerGroup normalMixer;
    public AudioMixerGroup lowMixer;

    private bool sfxDisabled = false;
    public List<AudioSource> audiosources = new List<AudioSource>();
    /*[Header("Audio Listner")]
    [SerializeField] AudioListener audioListener;*/
    public AudioSource currentPlaying;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
         // DontDestroyOnLoad(this.gameObject);

        // intialize each clip with audiosoure
        foreach (SoundClips s in soundClips)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            audiosources.Add(s.audioSource);

            s.audioSource.clip = s.clip;
            //s.audioSource.loop = s.loopClip;
            //s.audioSource.playOnAwake = s.PlayOnAwake;
            //s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.Volume;
            s.audioSource.playOnAwake = false;
            if (s.looping)
            {
                //   s.audioSource.loop = true;
            }
        }
        foreach (SoundClips s in musicClips)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            audiosources.Add(s.audioSource);

            s.audioSource.clip = s.clip;
            //s.audioSource.loop = s.loopClip;
            //s.audioSource.playOnAwake = s.PlayOnAwake;
            //s.audioSource.pitch = s.pitch;
            s.audioSource.volume = s.Volume;
            s.audioSource.playOnAwake = false;
            if (s.looping)
            {
                   s.audioSource.loop = true;
            }
        }



        foreach (ClipsRandom s in clipsRandom)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            // s.audioSource.clip = s.clip;
            s.audioSource.volume = s.Volume;
        }
    }

    public void MusicButtonPressed()
    {
        //  Play("button");
        if (musicMuted)
        {
            musicMuted = false;
          //  PlayMainMusic();
        }
        else
        {
            musicMuted = true;
            StopMainMusic();

        }
    }
    public void ChangeVolume(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeVolumeSound(float value)
    {
        for (int i = 0; i < audiosources.Count; i++)
        {
            audiosources[i].volume = value;
        }
    }
    public void PlaySFX(string name)
    {
        if (sfxDisabled)
            return;

        SoundClips s = Array.Find(soundClips, sound => sound.clipName == name); // Lambada expression
        if (s == null)
        {
            Debug.Log("Sound " + name + " not Found");
            return;
        }
        //if (s.looping)
        //{
        //    s.audioSource.loop = true;
        //}
        s.audioSource.enabled = true;

        s.audioSource.Play();

    }
    public void PlayMusic(string name)
    {
        if (sfxDisabled)
            return;

        SoundClips s = Array.Find(musicClips, sound => sound.clipName == name); // Lambada expression
        if (s == null)
        {
            Debug.Log("Sound " + name + " not Found");
            return;
        }
        if (s.looping)
        {
            s.audioSource.loop = true;
        }
        s.audioSource.enabled = true;

        s.audioSource.Play();

    }


    public void StopCurrent()
    {

        currentPlaying.Stop();
    }
    public float PlayRandomized(string name)
    {
        if (sfxDisabled)
            return 0f; // Return a default value to match the float return type

        ClipsRandom s = Array.Find(clipsRandom, sound => sound.clipName == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found");
            return 0f; // Return a default value to match the float return type
        }

        if (s.clips == null || s.clips.Length == 0)
        {
            Debug.Log("No clips available for sound " + name);
            return 0f; // Handle case where no clips are found
        }

        s.audioSource.clip = s.clips[Random.Range(0, s.clips.Length)];
        s.audioSource.Play(); // Assuming you want to play the sound
        return s.audioSource.clip.length; // Return the length of the randomly selected clip
    }

    public void StopSound(string name) // for stooping looping sounds
    {
        SoundClips s = Array.Find(soundClips, sound => sound.clipName == name); // Lambada expression
        if (s == null)
        {
            Debug.Log("Sound " + name + " not Found");
            return;
        }
        s.audioSource.enabled = false;

    }

    public void StopMainMusic()
    {
        musicSource.enabled = false;
    }

    public void PlayMainMusic()
    {
        musicSource.enabled = true;
    }

    #region Mixer Functions
    public void SetLowMixer()
    {
        musicSource.outputAudioMixerGroup = lowMixer;
    }

    public void SetnormalMixer()
    {
        musicSource.outputAudioMixerGroup = normalMixer;
    }
    #endregion

    #region AudioLisnter
    public void DisableAudioLisner()
    {
        AudioListener.volume = 0;
    }

    public void EnableAudioLisner()
    {
        AudioListener.volume = 1;
    }
    #endregion

    public void DisableSFX()
    {
        sfxDisabled = true;
    }

    public void EnableSFX()
    {
        sfxDisabled = false;
    }

    public void PlaySingle(AudioClip audioClip) // give it audio clip and will play it
    {
        if (sfxDisabled)
            return;

        effectsSource.PlayOneShot(audioClip);
    }

    public bool EffectSourceIsPlaying()
    {
        return effectsSource.isPlaying;
    }

    public void RandomizeSFX(params AudioClip[] clips) // params to send multible clips sebrated by a comma
    {                                                  // nice to avoid repating sounds
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        effectsSource.pitch = randomPitch;
        effectsSource.clip = clips[randomIndex];
        effectsSource.Play();
    }
}