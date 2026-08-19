using System.Collections;
using UnityEngine;

public class SaveSystem : ISaveSystem
    {
        private const string SAVE_KEY = "PlayerData";
        private Data data = new Data();

        public void Save()
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save(); 
            Debug.Log("Игра сохранена!");
        }

        public void Load()
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

        public Data GetData() => data;
    }
