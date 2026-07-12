using UnityEngine;

public class GameBalance : MonoBehaviour
{
    private int _amountKillEnemy = 0;
    private EnemyData EnemyData;
    private IEventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Instance.Get<IEventBus>();
        _eventBus?.Subscribe<AddScoreKillEvent>(AddKill);

        GetJson();
    }

    private void GetJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Balance/GameDate");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            EnemyData = JsonUtility.FromJson<EnemyData>(json);
        }
        else
        {
            Debug.LogError("File JSON not find in Resources");
        }
    }

    private void AddKill(AddScoreKillEvent addScoreKillEvent)
    {
        _amountKillEnemy++; 
        ChangeEnemySettings();
    }

    private void ChangeEnemySettings()
    {
        for (int i = 0; i < EnemyData.Balance.Count; i++)
        {
            if (_amountKillEnemy >= EnemyData.Balance[i].RequiredKill)
            {
                _eventBus.Publish(new EnemyUpdateEvent(EnemyData.Balance[i].EnemyHealh));
            }
        }
    }
}

