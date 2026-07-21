using System.Collections;
using UnityEngine;

namespace Assets.AirBatter.Scripts.GameLogic.Shop
{
    public class SaveShop : MonoBehaviour
    {


        private const string SAVE_KEY = "PlayerData";
        private Data data = new Data();

        private void Awake()
        {
            LoadGame();
        }

        public void SaveGame()
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save(); 
            Debug.Log("Игра сохранена!");
        }

        public void LoadGame()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
            {
                string json = PlayerPrefs.GetString(SAVE_KEY);
               
                data = JsonUtility.FromJson<Data>(json);
                Debug.Log("Игра загружена!");
            }
            else
            {
                Debug.Log("Сохранения нет, создаём новое.");
                data = new Data(); 
            }
        }

        public Data DataShop() => data;
    }
}