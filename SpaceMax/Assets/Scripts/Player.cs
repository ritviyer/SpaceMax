using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    FloatingJoystick joystick;
    Transform playerT;
    [SerializeField] GameObject aim;
    //public Thruster[] thruster;

    public Laser lr;
    [SerializeField] float speed;
    float turnSpeed = 45f;
    float tilt = 45f;

    public Camera mainCamera;
    Rigidbody rigidbody;
    bool moveCamera = true;

    float startTime;


    void Awake()
    {
        playerT = transform;
        joystick = FindObjectOfType<FloatingJoystick>();
        mainCamera = FindObjectOfType<Camera>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //movePlayer();
        //movePlayerTrial();
        aimLaser();
        Thrust();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x > (Screen.width / 2))
                Shoot();
        }
        if (Time.time - startTime > 20)
        {
            startTime = Time.time;
            if(speed < 500)
                StartCoroutine(IncreaseSpeed(250, speed + 50));
        }
    }

    IEnumerator IncreaseSpeed(int number, float finalSpeed)
    {
        int i = 0;
        while (i < number)
        {
            i++;
            speed += 0.2f;
            EventManager.SpeedIncrease();
            yield return 0;
        }
        speed = finalSpeed;
        EventManager.SpeedIncrease();
        (mainCamera.GetComponent<CameraController>()).CameraFix();
    }
    private void FixedUpdate()
    {
        movePlayerFixed();
    }
    //private void OnEnable()
    //{
    //    EventManager.onStartGame += Shoot;
    //}

    //private void OnDisable()
    //{
    //    EventManager.onStartGame -= Shoot;
    //}

    void movePlayerFixed()
    {
        if (moveCamera)
        {
            //(mainCamera.GetComponent<CameraController>()).FollowPlayer();
        }

        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        if (speed==0 && moveCamera)
        {
            moveHorizontal = Mathf.Lerp(0, joystick.Horizontal, Time.deltaTime * 100f);
            moveVertical = Mathf.Lerp(0, joystick.Vertical, Time.deltaTime * 100f);
            Vector3 newPosition = playerT.position + new Vector3(moveHorizontal, moveVertical, 0);
            playerT.position = Vector3.Lerp(playerT.position, newPosition, Time.deltaTime * 20f);
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPosition.position, Time.deltaTime * 5f);
        }


        //float moveHorizontal = turnSpeed * Time.deltaTime * joystick.Horizontal;
        //float moveVertical = turnSpeed * Time.deltaTime * joystick.Vertical;


        //Vector3 velocity = Vector3.one;
        //Vector3 toPos = new Vector3(playerT.position.x+moveHorizontal, playerT.position.y+moveVertical, playerT.position.z+speed * Time.deltaTime);
        //playerT.position = Vector3.SmoothDamp(transform.position, toPos, ref velocity, 0.05f);

        //playerT.position += playerT.forward * speed * Time.deltaTime;

        //playerT.rotation = Quaternion.Euler(moveVertical * -tilt, 0.0f, moveHorizontal * -tilt);

        float rotationX = moveVertical * -(tilt / 2);
        float rotationY = moveHorizontal * (tilt / 2f);
        float rotationZ = moveHorizontal * -(tilt * 1.5f);
        
        rotationX = Mathf.Clamp(rotationX, -45, 45);
        rotationY = Mathf.Clamp(rotationY, -45, 45);
        rotationZ = Mathf.Clamp(rotationZ, -60, 60);

        if (rotationZ != 0)
        {
            Quaternion newRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            playerT.rotation = Quaternion.Slerp(playerT.rotation, newRotation, Time.deltaTime);
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            playerT.rotation = Quaternion.Slerp(playerT.rotation, newRotation, Time.deltaTime/3);
        }
        

        //playerT.Translate(moveHorizontal*Time.deltaTime, moveVertical*Time.deltaTime, 0);
        playerT.position += playerT.forward * speed * Time.deltaTime;
    }

    void Thrust()
    {
        //thrust when required
    }
    void aimLaser()
    {
        //aim.SetActive(false);
        //for(int i = 1; i<101; i++)
        //{
        //    aim.transform.position = new Vector3(aim.transform.position.x, aim.transform.position.y, playerT.position.z + i);
        //    bool target = lr.Raycast(aim.transform.position);
        //    if (target)
        //        aim.SetActive(true);
        //}

        
        Vector3 target = lr.Raycast(transform.position, transform.forward);
        aim.transform.position = target;
        //aim.transform.position -= Vector3.forward*5;

        //Debug.DrawRay(transform.position, transform.forward*100f, Color.yellow);
        //float maxDistance = 100f;
        //Ray ray = new Ray(lr.transform.position, lr.transform.forward);
        //RaycastHit hit;
        //if (Physics.Raycast(lr.transform.position, hit, maxDistance))
        //{
        //    aim.transform.position = viewport.WorldToViewportPoint(hit.point);
        //}
        //else
        //{
        //    aim.transform.position = viewport.WorldToViewportPoint(lr.transform.forward * maxDistance);
        //}

    }
    void Shoot()
    {
        lr.FireLaserPlayer(aim.transform.position);
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float speedFloat)
    {
        speed = speedFloat;
    }
    public void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
    public void MoveCamera(bool toMove)
    {
        moveCamera = toMove;
    }
}
