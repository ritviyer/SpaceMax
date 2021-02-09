using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameMenu;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerStartPosition;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemyStartPosition;

    private void Start()
    {
        ShowMainMenu();
    }
    private void OnEnable()
    {
        EventManager.onStartGame += ShowGameMenu;
      //  EventManager.onHealthDamage += ShowMainMenu;
    }
    private void OnDisable()
    {
        EventManager.onStartGame -= ShowGameMenu;
       // EventManager.onHealthDamage -= ShowMainMenu;
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
    }

    void ShowGameMenu()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
        Instantiate(playerPrefab, playerStartPosition.transform.position, playerStartPosition.transform.rotation);
        EventManager.PlayerSpawn();
        Instantiate(enemyPrefab, enemyStartPosition.transform.position, enemyStartPosition.transform.rotation);
    }
}
