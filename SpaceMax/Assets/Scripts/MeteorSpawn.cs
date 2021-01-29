using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

public class MeteorSpawn : MonoBehaviour
{
    public GameObject vfx;
    Player player;
    float pSpeed;
    GameObject objVFX;

    float startTime;
    float meteorSpeed;
    int numberOfMeteors;

    private void Start()
    {
        startTime = Time.time;
    }

    public void SpawnMeteor()
    {
        player = FindObjectOfType<Player>();
        pSpeed = player.GetSpeed();

        int level = GetLevel();
        int random = Random.Range(5, 16);
        StartCoroutine(SpawnTimes(random, level));
    }
    void FixedUpdate()
    {
        float timeTemp = (Time.time - startTime)%30;
        if(timeTemp>=29.95 || timeTemp == 0)
        {
            SpawnMeteor();
        }
    }

    IEnumerator SpawnTimes(int number, int level)
    {
        int j = 0;
        while (j < number)
        {
            j++;
            List<Vector3> endPositions = CalculateEnd(level);

            for (int i = 0; i < numberOfMeteors; i++)
            {
                Vector3 startPos = CalculateStart(endPositions[i], 300);
                objVFX = Instantiate(vfx, startPos, Quaternion.identity) as GameObject;
                (objVFX.GetComponent<MeteorProjectile>()).SetSpeed(meteorSpeed);
                RotateTo(objVFX, endPositions[i]);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    int GetLevel()
    {
        if (Time.time - startTime <= 90)
            return 1;
        else if ((Time.time - startTime > 90) && (Time.time - startTime <= 240))
            return 2;
        else if ((Time.time - startTime > 240) && (Time.time - startTime <= 480))
            return 3;
        else if ((Time.time - startTime > 480) && (Time.time - startTime <= 840))
            return 4;
        return 5;
    }
    Vector3 CalculateStart(Vector3 end, float distance)
    {
        List<int> yValues = new List<int>(new int[] { 50, 100, 150});
        List<int> zValues = new List<int>(new int[] { 100, 150, 200, 250});
        float yValue = yValues[Random.Range(0,yValues.Count)];
        float zValue = zValues[Random.Range(0,zValues.Count)];
        float xValue = CalculateDistance(yValue, zValue, distance);
        Vector3 startPosition;
        if(Random.Range(0,2) == 0)
            startPosition = new Vector3(end.x - xValue, end.y + yValue, end.z+ zValue);
        else
            startPosition = new Vector3(end.x + xValue, end.y + yValue, end.z + zValue);
        return startPosition;
    }

    float CalculateDistance(float y, float z, float distance)
    {
        float x = Mathf.Sqrt((distance * distance) - (y * y) - (z * z));
        return x;
    }

    List<Vector3> CalculateEnd(int level)
    {
        float sec;

        if (level == 1)
        {
            sec = 3f;
            if (Random.Range(0, 2) == 0)
                numberOfMeteors = 1;
            else
                numberOfMeteors = 2;
        }
        else if (level == 2)
        {
            if (Random.Range(0, 2) == 0)
            {
                sec = 3f;
                numberOfMeteors = 3;
            }
            else
            {
                sec = 2f;
                numberOfMeteors = 2;
            }
        }
        else if (level == 3)
        {
            sec = 2f;
            if (Random.Range(0, 2) == 0)
                numberOfMeteors = 3;
            else
                numberOfMeteors = 4;
        }
        else if (level == 4)
        {
            if (Random.Range(0, 2) == 0)
            {
                sec = 2f;
                numberOfMeteors = 4;
            }
            else
            {
                sec = 1.5f;
                numberOfMeteors = 3;
            }
        }
        else
        {
            sec = 1.5f;
            if (Random.Range(0, 2) == 0)
                numberOfMeteors = 4;
            else
                numberOfMeteors = 5;
        }

        if (sec == 3f)
            meteorSpeed = 100f;
        else if (sec == 2f)
            meteorSpeed = 150f;
        else
            meteorSpeed = 200f;

        int randPos = Random.Range(10, 21);
        Vector3 endPosition = player.transform.position + player.transform.forward * sec * pSpeed;
        Vector3 endPositionLeft = endPosition - player.transform.right*randPos*sec;
        Vector3 endPositionRight = endPosition + player.transform.right* randPos * sec;
        Vector3 endPositionUp = endPosition + player.transform.up* randPos * sec;
        Vector3 endPositionDown = endPosition - player.transform.up* randPos * sec;
        List<Vector3> positions = new List<Vector3>(){endPosition, endPositionLeft, endPositionRight, endPositionUp, endPositionDown};

        if (numberOfMeteors == 5)
            return positions;
        else if(sec == 3 && numberOfMeteors > 1)
        {
            for(int i = 0; i < numberOfMeteors; i++)
            {
                int element = Random.Range(i, positions.Count);
                Vector3 temp = positions[element];
                positions[element] = positions[i];
                positions[i] = temp;
            }
        }
        else
        {
            for (int i = 1; i < numberOfMeteors; i++)
            {
                int element = Random.Range(i, positions.Count);
                Vector3 temp = positions[element];
                positions[element] = positions[i];
                positions[i] = temp;
            }
        }
        return positions;
    }
    void RotateTo(GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}
