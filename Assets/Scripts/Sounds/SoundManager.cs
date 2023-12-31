using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup masterMixer;
    public Sound[] sounds;

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.rolloffMode = sound.audioRolloffMode;
            if (sound.spatialBlend == 1) { sound.source.SetCustomCurve(sound.audioSourceCurveType, sound.audioSourceAnimationCurve); }
            sound.source.minDistance = sound.minDistance;
            sound.source.maxDistance = sound.maxDistance;
        }

        
    }

    void Start()
    {
        UploadPlayerPrefs();
    }

    public void Play(string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            sound.source.Play();
        }

    }

    public void Stop(string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            sound.source.Stop();
        }
    }

    public AudioSource LocateAudioSource(string name, Transform newParent)
    {
        //Debug.Log(Array.Find(sounds, sound => sound.name == name).name);
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            string oldName = newParent.gameObject.name;
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            AudioSource newAudioSource = newParent.gameObject.AddComponent<AudioSource>();
            newAudioSource.name = sound.name;
            newAudioSource.clip = sound.clip;

            newAudioSource.volume = sound.volume;
            newAudioSource.pitch = sound.pitch;
            newAudioSource.loop = sound.loop;
            newAudioSource.spatialBlend = sound.spatialBlend;
            newAudioSource.rolloffMode = sound.audioRolloffMode;
            newAudioSource.SetCustomCurve(sound.audioSourceCurveType, sound.audioSourceAnimationCurve);
            newAudioSource.minDistance = sound.minDistance;
            newAudioSource.maxDistance = sound.maxDistance;

            newParent.gameObject.name = oldName;
            return newAudioSource;
        }
        return null;
    }

    public AudioSource FindSound(string name)
    {
        if (Array.Find(sounds, sound => sound.name == name) != null)
        {
            return Array.Find(sounds, sound => sound.name == name).source;
        }

        return null;
    }

    public void UploadPlayerPrefs()
    {
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.01f);
        masterMixer.audioMixer.SetFloat("MusicVol", Mathf.Log10(musicVolume) * 20);

        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.01f);
        masterMixer.audioMixer.SetFloat("SfxVol", Mathf.Log10(sfxVolume) * 20);
    }
}
