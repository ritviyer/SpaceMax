using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    FloatingJoystick joystick;
    private Player player;
    private Vector3 lastPosition;
    private float distance;

    bool move = false;
    private void OnEnable()
    {
        EventManager.onPlayerTeleport += Move;
        EventManager.onPlayerSpawn += FindPlayer;
    }    
    private void OnDisable()
    {
        EventManager.onPlayerTeleport -= Move;
        EventManager.onPlayerSpawn -= FindPlayer;
    }
    void FindPlayer()
    {
        player = FindObjectOfType<Player>();
    }
    private void FixedUpdate()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        if (player)
        {
            float distancex = player.transform.position.x - lastPosition.x;
            float distancey = player.transform.position.y - lastPosition.y;
            float distancez = player.transform.position.z - lastPosition.z;
            transform.position = new Vector3(transform.position.x + distancex, transform.position.y + distancey, transform.position.z + distancez);
            lastPosition = player.transform.position;
            RotateCamera();
        }
    }

    void RotateCamera()
    {
        float turnSpeed = 45f;
        float tilt = 45f;
        joystick = FindObjectOfType<FloatingJoystick>();
        float moveHorizontal = turnSpeed * Time.deltaTime * joystick.Horizontal;
        float moveVertical = turnSpeed * Time.deltaTime * joystick.Vertical;


        Quaternion newRotation = Quaternion.Euler(moveVertical * -(tilt / 2), moveHorizontal * (tilt / 2f), transform.rotation.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime);
    }

    public void CameraFix()
    {
        //0.25 back every speed increase
        StartCoroutine(MoveCamera(100, 0.75f));
        float pSpeed = player.GetSpeed();
        if (pSpeed > 300f)
            Camera.main.farClipPlane = pSpeed * 3;
    }

    void Move(bool toMove)
    {
        move = toMove;
        if (move)
            ZoomIn();
    }
    void ZoomIn()
    {
        StartCoroutine(MoveCamera(100,-1f));
    }
    IEnumerator MoveCamera(int number, float change)
    {
        int i = 0;
        Vector3 changeZ = new Vector3(0, 0, change/number);
        while (i < number)
        {
            i++;
            transform.position += changeZ;
            yield return new WaitForSeconds(0f);
        }
    }
}
