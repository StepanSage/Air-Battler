using UnityEngine;
using UnityEngine.UI;

public class SettingView : MonoBehaviour
{
    [SerializeField] private Slider _audio;
    [SerializeField] private Slider _music;

    private IAudioManager _audioManager;

    private void Start()
    {
        _audioManager = ServiceLocator.Instance.Get<IAudioManager>();

        if (_audioManager == null)
        {
            Debug.LogError("IAudioManager не найден в ServiceLocator!");
            return;
        }

        _audio.value = _audioManager.SfxVolume;
        _music.value = _audioManager.MusicVolume;

        _audio.onValueChanged.AddListener(AudioChangeVolum);
        _music.onValueChanged.AddListener(MusicChangeVolum);
    }

    private void AudioChangeVolum(float value)
    {
        _audioManager?.SetSfxvolum(value);
    }

    private void MusicChangeVolum(float value)
    {
        _audioManager?.SetMusicvolum(value);
    }

    private void OnDestroy()
    {
        // Отписываемся от событий (хорошая практика)
        _audio.onValueChanged.RemoveListener(AudioChangeVolum);
        _music.onValueChanged.RemoveListener(MusicChangeVolum);
    }
}