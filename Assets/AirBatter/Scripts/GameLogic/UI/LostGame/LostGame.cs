using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LostGame : MonoBehaviour
{
    [SerializeField] private GameObject _playerHUD;
    [SerializeField] private GameObject _loseHud;
    [SerializeField] private Button _restart;
    [SerializeField] private LodingShutter _lodingShutter;

    private IEventBus _eventBus;

    private void Start()
    {
        _restart.onClick.AddListener(() => RestartScene());
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();

        _eventBus?.Subscribe<LostGameEvent>(RestartGames);
    }

    private void RestartGames(LostGameEvent lostGameEvent)
    {
       
        if(lostGameEvent == null) return;

       
        if (lostGameEvent.AmmountHelth - 1  <= 0)
        {
            Debug.Log("Ивент на проигрышь призошел ");
            _loseHud?.SetActive(true);
        }
           
    }

    private void RestartScene()
    {
        _lodingShutter?.Launch(LuntchRestart);
    }

    private void LuntchRestart()
    {
        _playerHUD?.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
