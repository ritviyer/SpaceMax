using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    Player player;

    public void RotateMe()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(Rotate45(16));
        //just adding temp for convinience
        Enemy enemy = FindObjectOfType<Enemy>();
        enemy.transform.position = (Vector3.forward * 150) + player.transform.position;
    }
    IEnumerator Rotate45(int number)
    {
        int i = 0;
        while (i < number)
        {
            i++;
            player.transform.Rotate(0f, 0f, 45f);
            yield return 0;
        }
    }
}
