using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    private static PlayerProfile _instance;

    private int currency = 150; // Initialize currency to 150
    private int enemiesKilled;
    private int xpEarned;

    public int Currency => currency;
    public int EnemiesKilled => enemiesKilled;
    public int XPEarned => xpEarned;

    public static PlayerProfile Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject profileObject = new GameObject("PlayerProfile");
                _instance = profileObject.AddComponent<PlayerProfile>();
                DontDestroyOnLoad(profileObject);
            }
            return _instance;
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public void IncreaseEnemiesKilled()
    {
        enemiesKilled++;
    }

    public void AddXP(int amount)
    {
        xpEarned += amount;
    }
}
