using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Vector2 _endZoom;

    private BaseScreen _baseScreen;
    private Vector2 _startZoom;
    private IGamePause _gamePause;
    private Ability _ability;

    private void Start()
    {
        _gamePause = ServiceLocator.Instance.Get<IGamePause>();
        _baseScreen = GetComponentInParent<UiCards>();
        _ability = GetComponent<Ability>();
        _startZoom = transform.localScale;
    }

    private void OnDisable() => Destroy(gameObject);
    

    public void OnPointerDown(PointerEventData eventData) => transform.localScale = _endZoom;

    public void OnPointerExit(PointerEventData eventData) => transform.localScale = _startZoom;

    public void OnPointerClick(PointerEventData eventData)
    {

        transform.localScale = _startZoom;
        _ability.Useing();
        _gamePause.Play();
        _baseScreen.Hide();
    }

}

public struct OnAbilitySelect: IEvent { }
