using UnityEngine;

public class GamePause : IGamePause
{
    public GamePause()
    {

    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Play()
    {
        Time.timeScale = 1;
    }
}
