using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComtroller : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;

    private IAudioManager _audioManager;
    private void Start()
    {
        _audioManager=  ServiceLocator.Instance.Get<IAudioManager>();
    }

    private void Update()
    {
        for (int i = 0; i < _audioSources.Length; i++)
        {
            _audioSources[i].volume = _audioManager.MusicVolume;
        }
    }
}
