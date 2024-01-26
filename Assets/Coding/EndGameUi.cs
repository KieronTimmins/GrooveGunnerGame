using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    public TMP_Text killCountText;
    public TMP_Text harmonyTokensText;  // TMP Text for displaying Harmony Tokens

    void Start()
    {
        if (killCountText != null)
        {
            killCountText.text = "Enemies Killed: " + PlayerProfile.Instance.EnemiesKilled.ToString();
        }

        if (harmonyTokensText != null)
        {
            harmonyTokensText.text = "Harmony Tokens: " + PlayerProfile.Instance.HarmonyTokens.ToString();
        }
    }
}