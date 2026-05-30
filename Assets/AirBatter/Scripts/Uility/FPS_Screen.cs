using UnityEngine;
using TMPro;

public class FPS_Screen : BaseScreen
{
    [SerializeField] private TMP_Text _fpsText;

    private float deltaTime = 0.0f;

    private void Start() => InvokeRepeating("UpdateFPS", 0.0f, 0.25f);

    private void Update() => deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

    private void UpdateFPS()
    {
        float fps = 1.0f / deltaTime;
        _fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
    }

}
