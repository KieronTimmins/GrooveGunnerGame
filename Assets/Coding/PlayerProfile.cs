using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProfile : MonoBehaviour
{
    private static PlayerProfile _instance;

    private int currency = 150; // Initialize currency to 150
    public int enemiesKilled;
    private int xpEarned;

    public int Currency => currency;
    public int EnemiesKilled => enemiesKilled;
    public int XPEarned => xpEarned;

    // New property for harmony tokens
    public int HarmonyTokens => CalculateHarmonyTokens();

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

    // Calculate harmony tokens based on the currency
    private int CalculateHarmonyTokens()
    {
        return Mathf.CeilToInt(currency / 10000f);
    }
}
