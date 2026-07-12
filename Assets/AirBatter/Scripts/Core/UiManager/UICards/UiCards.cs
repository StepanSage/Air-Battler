using UnityEngine;

public class UiCards : BaseScreen
{
    [SerializeField] private GameObject[] _cards;
    [SerializeField] private Transform[] _pointSpawnCards;
    [SerializeField] private Transform _contener;

    private void OnEnable()
    {
        CreatCards();  
    }

    private void CreatCards()
    {
        for (int i = 0; i < _pointSpawnCards.Length; i++)
        {
            var index = UnityEngine.Random.Range(0, _cards.Length);
            var cards = Instantiate(_cards[index], _pointSpawnCards[i].position, Quaternion.identity);
            cards.transform.parent = _contener;
            
        }
    }
}
