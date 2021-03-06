﻿using UnityEngine;

using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource MainCameraAudioSource = null;

    [SerializeField]
    List<AudioClip> SoundFiles;

    static AudioManager _instance = null;
    public static AudioManager Instance { get { return _instance; } }

    public enum SoundNames
    {
        music_01,
        music_02,
        efx_ouch,
        efx_jump,
        efx_fatBlast,
        efx_pupPickUp,
        efx_trampoline,
        efx_waterSplash
    };

    private Dictionary<string, AudioClip> m_ClipToPlay = new Dictionary<string, AudioClip>();


	// Use this for initialization
	void Start ()
    {
        _instance = this;

        if (SoundFiles != null)
        {
            foreach (AudioClip _clip in SoundFiles)
            {
                if(_clip == null)
                {
                    Debug.LogError("SoundClip file was null!");
                    return;
                }

                m_ClipToPlay.Add(_clip.name, _clip);
            }
        }
	}

    public bool PlaySoundEffect(AudioSource _source)
    {
        if (_source.clip != null)
        {
            _source.Play();
            return true;
        }

        Debug.LogError(_source.gameObject.name + " does not have a AudioClip!");

        return false;
    }

    public bool PlaySoundEffect(string efxName, AudioSource _source)
    {
        AudioClip _clip;
        if (m_ClipToPlay.TryGetValue(efxName, out _clip))
        {
            _source.clip = _clip;
            _source.Play();
            return true;
        }

        Debug.LogError("SoundEffect for " + efxName + " were not found!");

        return false;
    }

    public bool PlayMusic(string musicName)
    {
        AudioClip _clip;
        if (m_ClipToPlay.TryGetValue(musicName, out _clip))
        {
            if (MainCameraAudioSource.isPlaying)
                MainCameraAudioSource.Stop();

            MainCameraAudioSource.clip = _clip;
            MainCameraAudioSource.Play();
            return true;
        }

        Debug.LogError("Music for " + musicName + " were not found!");

        return false;
    }
}
