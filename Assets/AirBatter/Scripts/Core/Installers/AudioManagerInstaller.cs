using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerInstaller : Installer
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _musicParent = "musicVolume";
    [SerializeField] private string _sfxParent = "SfxVolume";

    private IAudioManager instance;
   

    public override void Install(ServiceLocator serviceLocator)
    {
        var bus = serviceLocator.Get<IEventBus>();
        instance ??= new AudioManager(bus, _audioMixer, _musicParent, _sfxParent);
        serviceLocator.Rigister<IAudioManager>(instance);
    }

    public override void Uninstall(ServiceLocator serviceLocator)
    {
        serviceLocator.UnRigister<IAudioManager>(instance);
        (instance as AudioManager)?.Dispose();
        instance = null;
    }
}
