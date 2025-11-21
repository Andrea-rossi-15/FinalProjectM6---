using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioSources
    {
        HIT,
        HEALING,
        COIN,
        DEATH,
        LOOSE,
    }
    [SerializeField] private AudioClip[] _soundList;
    private static AudioManager _istance;
    private AudioSource _audioSource;


    private void Awake()
    {
        _istance = this;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioSources _sound, float _volume = 1)
    {
        _istance._audioSource.PlayOneShot(_istance._soundList[(int)_sound], _volume);
    }
}

