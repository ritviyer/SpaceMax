using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TeleportandSpawn : MonoBehaviour
{
    [SerializeField] GameObject teleport;
    [SerializeField] GameObject spawn;
    Player player;
    float playerSpeed;
    GameObject temp;
    public void Teleport()
    {
        Camera.main.GetComponent<RotateSky>().enabled = true;
        player = FindObjectOfType<Player>();
        playerSpeed = player.GetSpeed();
        StartCoroutine(StopPlayer(50, playerSpeed/50));
        Invoke("ScaleDown", 2f);
    }

    IEnumerator StopPlayer(int number, float decrease)
    {
        int i = 0;
        while (i < number)
        {
            i++;
            player.SetSpeed(player.GetSpeed()-decrease);
            if (i == number)
            {
                player.MoveCamera(false);
                EventManager.PlayerTeleport(true);
                GameObject go = Instantiate(teleport, player.transform.position, Quaternion.identity);
            }
            yield return 0;
        }
    }
    void ScaleDown()
    {
        StartCoroutine(ScaleDownPlayer(100));
    }
    IEnumerator ScaleDownPlayer(int number)
    {
        int i = 0;
        while (i < number)
        {
            i++;
            player.transform.localScale = player.transform.localScale * 0.98f;
            yield return new WaitForSeconds(0.01f);
            if (i == number)
            {
                player.DisablePlayer();
                Invoke("CameraZoom", 2f);
            }

        }
    }
    void CameraZoom()
    {
        StartCoroutine(CameraZoomEnum(200));

    }
    IEnumerator CameraZoomEnum(int number)
    {
        int i = 0;
        while (i < number)
        {
            i++;
            Camera.main.transform.position += Vector3.forward * 100;
            if (i <=number/2)
                Camera.main.fieldOfView += 1;
            else
                Camera.main.fieldOfView -= 1;
            yield return new WaitForSeconds(0.0f);
            if(i==number)
                Camera.main.GetComponent<RotateSky>().enabled = false;
        }
    }
}
