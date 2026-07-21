using UnityEngine;

public class BootstrapGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject _gamePlay;
    [SerializeField] private GameObject _menu;
    [SerializeField] private LodingShutter _lodingShutter;
   
    private void Awake()
    {
       _gamePlay?.SetActive(false) ;
       _menu?.SetActive(true);
        
    }

    
}
