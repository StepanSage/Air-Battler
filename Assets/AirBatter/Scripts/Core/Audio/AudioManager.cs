using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : IAudioManager
{
    private readonly AudioMixer _audioMixer;
    private readonly AudioSource _musicSource;
    private readonly AudioSource _sfxSource;
    private readonly GameObject _rootGO;
    private readonly string _musicParam;
    private readonly string _sfxParam;

    private AudioClip _nextClip;
    private bool _fadingOut;
    private bool _fadingIn;
    private IEventBus _eventBus;

    public float MusicVolume { get; private set; } = 1f;

    public float SfxVolume { get; private set; } = 1f;

    public AudioManager(IEventBus eventBus, AudioMixer audioMixer,  string musicParam, string sfxVolume)
    {
        _eventBus = eventBus;
        _audioMixer = audioMixer;
        _musicParam = musicParam;
        _sfxParam = sfxVolume;

        _rootGO = new GameObject("[AudioManager]");
        Object.DontDestroyOnLoad(_rootGO);

        _rootGO.AddComponent<AudioManagerUpdater>().Init(this);

        _musicSource = _rootGO.AddComponent<AudioSource>();
        _musicSource.playOnAwake = false;
        _musicSource.loop = true;

        _sfxSource = _rootGO.AddComponent<AudioSource>();
        _sfxSource.playOnAwake = false;


        ApplayMixerVolume(_musicParam, MusicVolume);
        ApplayMixerVolume(_sfxParam, SfxVolume);



    }

    public void SetMusicvolum(float volum)
    {
        MusicVolume = Mathf.Clamp01(volum);
        ApplayMixerVolume(_musicParam, MusicVolume);
    }

    public void SetSfxvolum(float volum)
    {
        SfxVolume = Mathf.Clamp01(volum);
        ApplayMixerVolume(_sfxParam, SfxVolume);
    }


    public void PlayMusic(AudioClip clip, bool loop = false, float fadeSecong = 0)
    {
        if (fadeSecong > 0f && _musicSource.isPlaying)
        {
            _nextClip = clip;
            return;
        }

        _musicSource.clip = clip;
        _musicSource.loop = loop;
        _musicSource.volume = 1f;
        _musicSource.Play();

    }
    public void StopMusic(float fadeSecong = 0)
    {
        if (fadeSecong > 0f && _musicSource.isPlaying)
        {
            _nextClip = null;
            return;
        }

        _musicSource.Stop();
        _nextClip = null;
    }


    public void PlaySfx(AudioClip clip, float volum = 1)
    {
        if (clip != null) return;

        _sfxSource?.PlayOneShot(clip, Mathf.Clamp01(volum));

    }

    public void Update(float deltaTime)
    {

    }

    public void Dispose()
    {
        if(_rootGO != null)
            Object.Destroy(_rootGO);
    }

    private void ApplayMixerVolume(string param, float value)
    {
        var dB = (value <= 0.0001f) ? -80f : Mathf.Log10(value) * 20f;
        _audioMixer.SetFloat(param, dB);
    }

}
