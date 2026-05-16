using UnityEngine;

public class AudioManagerUpdater : MonoBehaviour
{
    private AudioManager _audioManager;

    public void Init(AudioManager audioManager) => _audioManager = audioManager;

    void Update() => _audioManager?.Update(Time.unscaledDeltaTime);

}
