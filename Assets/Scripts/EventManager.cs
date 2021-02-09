using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void StartGameDelegate();
    public static StartGameDelegate onStartGame;
    public static StartGameDelegate onPlayerSpawn;
    public static StartGameDelegate onPlayerDeath;
    public static StartGameDelegate onSpeedIncrease;
    
    public delegate void HealthDamageDelegate(float amt);
    public static HealthDamageDelegate onHealthDamage;    
    
    public delegate void ScorePointsDelegate(int amt);
    public static ScorePointsDelegate onScorePoints;

    public delegate void PlayerTeleportDelegate(bool move);
    public static PlayerTeleportDelegate onPlayerTeleport;

    public static void StartGame()
    {
        if (onStartGame != null)
            onStartGame();
    }    
    
    public static void PlayerDeath()
    {
        if (onPlayerDeath != null)
            onPlayerDeath();
    }
    public static void PlayerSpawn()
    {
        if (onPlayerSpawn != null)
            onPlayerSpawn();
    }
    public static void SpeedIncrease()
    {
        if (onSpeedIncrease != null)
            onSpeedIncrease();
    }
    public static void HealthDamage(float health)
    {
        if (onHealthDamage != null)
            onHealthDamage(health);
    }

    public static void ScorePoints(int amt)
    {
        if (onScorePoints != null)
            onScorePoints(amt);
    }

    public static void PlayerTeleport(bool move)
    {
        if (onPlayerTeleport != null)
            onPlayerTeleport(move);
    }
}
