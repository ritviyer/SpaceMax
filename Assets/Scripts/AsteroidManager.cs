using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] List<GameObject> asteroids;

    private Player player;
    private Vector3 lastPosition;

    Vector3 screenPoint;
    bool onScreen;
    float pSpeed;

    private void OnEnable()
    {
        EventManager.onPlayerSpawn += FindPlayer;
        EventManager.onSpeedIncrease += GetPlayerSpeed;
    }
    private void OnDisable()
    {
        EventManager.onPlayerSpawn -= FindPlayer;
        EventManager.onSpeedIncrease -= GetPlayerSpeed;
    }
    void FindPlayer()
    {
        player = FindObjectOfType<Player>();
        GetPlayerSpeed();
        lastPosition = player.transform.position;
    }
    void GetPlayerSpeed()
    {
        pSpeed = player.GetSpeed();
    }

    void FixedUpdate()
    {
        if(player)
            instantiateAsteroid();
    }

    void instantiateAsteroid()
    {
        float xSpeed = pSpeed*1.8f + 820;
        float ySpeed = pSpeed*1.8f + 320;
        float zSpeed = (pSpeed * Time.deltaTime *50)*5;

        int numOfAsteroids;
        if (pSpeed >= 100f && pSpeed <= 300f)
            numOfAsteroids = 1;
        else
            numOfAsteroids = 2;
        for (int i = 0; i < numOfAsteroids; i++)
        {
            float xPos = Random.Range(lastPosition.x - xSpeed, lastPosition.x + xSpeed);
            float yPos = Random.Range(lastPosition.y -ySpeed, lastPosition.y + ySpeed);
            float zPos = Random.Range(lastPosition.z + zSpeed, lastPosition.z + zSpeed);
            if (i == numOfAsteroids - 1)
            {
                if(pSpeed%100 == 0)
                {
                    CreateAsteroid(xPos, yPos, zPos);
                }
                else
                {
                    int ran = Random.Range(0, 100);
                    if (ran >= 50)
                    {
                        CreateAsteroid(xPos, yPos, zPos);
                    }
                }
            }
            else
            {
                CreateAsteroid(xPos, yPos, zPos);
            }
            lastPosition = player.transform.position;
            }
    }
    void CreateAsteroid(float xPos, float yPos, float zPos)
    {
        Vector3 position = new Vector3(xPos, yPos, zPos);
        screenPoint = Camera.main.WorldToViewportPoint(position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            int random = Random.Range(0, asteroids.Count - 1);
            Instantiate(asteroids[random], position, Quaternion.identity, transform);
        }
        else
        {
            Debug.Log("Destroyed");
        }
    }
}
