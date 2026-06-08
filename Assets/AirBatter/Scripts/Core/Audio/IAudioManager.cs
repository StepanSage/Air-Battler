public interface IAudioManager: IService 
{
    public float MusicVolume { get;} 
    public float SfxVolume { get;}

    public void SetMusicvolum(float volum);
    public void SetSfxvolum(float volum);

    public void PlayMusic(UnityEngine.AudioClip clip, bool loop = false, float fadeSecong=0f);
    public void StopMusic(float fadeSecong = 0f);

    /// <summary>
    /// plays SFX once
    /// </summary>
    public void PlaySfx(UnityEngine.AudioClip clip, float volum = 1f);



}
