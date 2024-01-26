using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    // Static variable to keep track of monster kills
    public static int monstersKilled = 0;

    // Method to increment the monster kill count
    public static void MonsterKilled()
    {
        monstersKilled++;
        Debug.Log("Monsters killed: " + monstersKilled);
    }
}