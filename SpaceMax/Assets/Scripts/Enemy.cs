using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
//using TreeEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    Transform target;
    public Laser laser;

    float movementSpeed;

    float raycastOffset = 2.5f;
    float detectionDistance = 150f;

    Vector3 hitPosition;
    float rotationalDamp = 0.5f;
    Vector3 newPosition;

    Player player;
    int x;
    int y;

    void Start()
    {
        player = FindObjectOfType<Player>();
        target = player.transform;
        //newPosition = transform.position;
        GetPlayerSpeed();

        //StartCoroutine(MoveRoutine());
        //StartCoroutine(MovePerSec());
    }
    private void OnEnable()
    {
        EventManager.onSpeedIncrease += GetPlayerSpeed;
    }
    private void OnDisable()
    {
        EventManager.onSpeedIncrease -= GetPlayerSpeed;
    }
    void GetPlayerSpeed()
    {
        movementSpeed = player.GetSpeed();
    }
    void FixedUpdate()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
        //Move();
        if (target)
        {
            transform.LookAt(target);
            if (InFront())
            {
                if(Random.Range(0,100)<100)
                    laser.FireLaser(hitPosition, target);
            }
        }
        PathFinding();
    }



    IEnumerator MoveRoutine()
    {
        while (true)
        {
            //Move();
            x = Random.Range(-1, 1);
            y = Random.Range(-1, 1);
            yield return new WaitForSeconds(4f);
        }
    }
    void Move()
    {
        if (Random.Range(1, 100) > 50)
        {
            //float x = Random.Range(-8, 8) * 10;
            //float y = Random.Range(-1, 3) * 10;
            //float z = Random.Range(5, 10) * 10;

            Vector3 temp = new Vector3(10,10,movementSpeed);
            //newPosition = new Vector3(target.position.x + x, target.position.y + y, target.position.z + z);
            //newPosition = new Vector3(target.position.x + x, target.position.y + y, movementSpeed);
            //newPosition = Vector3.MoveTowards(transform.position, temp, movementSpeed);
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime*10f;
            //transform.position += temp;
            transform.position = Vector3.Lerp(transform.position, transform.position + temp, Time.deltaTime);
        }
    }
    IEnumerator MovePerSec()
    {
        while (true)
        {
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
            yield return 0;
        }
    }

    bool InFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        //angle used was 45
        if (Mathf.Abs(angle) <=225 && Mathf.Abs(angle) >= 135)
        {
            Debug.DrawLine(transform.position, target.position,Color.green);
            if(LineOfSight())
                return true;
        }
        //Debug.DrawLine(transform.position, target.position,Color.yellow);
        return false;
    }

    bool LineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = target.position - laser.transform.position;
        if (Physics.Raycast(laser.transform.position, direction, out hit, laser.LaserDistance()*2)){
            if (hit.transform.CompareTag("Player"))
            {
                hitPosition = hit.transform.position;
                Debug.DrawRay(laser.transform.position, direction, Color.red);
                return true;
            }
        }
        return false;
    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 enemyOffset = Vector3.zero;
        Vector3 left = transform.position - Vector3.right * raycastOffset;
        Vector3 center = transform.position;
        Vector3 right = transform.position + Vector3.right * raycastOffset;

        Debug.DrawRay(left, Vector3.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, Vector3.forward * detectionDistance, Color.cyan);

        if ((Physics.Raycast(left, Vector3.forward, out hit, detectionDistance)
            && Physics.Raycast(center, Vector3.forward, out hit, detectionDistance)
            && Physics.Raycast(right, Vector3.forward, out hit, detectionDistance))
            || (!Physics.Raycast(left, Vector3.forward, out hit, detectionDistance)
            && Physics.Raycast(center, Vector3.forward, out hit, detectionDistance)
            && !Physics.Raycast(right, Vector3.forward, out hit, detectionDistance)))
        {
            if (Random.Range(0, 1) == 0)
                enemyOffset += Vector3.right + Vector3.forward;
            else
                enemyOffset -= Vector3.right + Vector3.forward;
        }
        else if ((Physics.Raycast(left, Vector3.forward, out hit, detectionDistance)
            && Physics.Raycast(center, Vector3.forward, out hit, detectionDistance))
            || Physics.Raycast(left, Vector3.forward, out hit, detectionDistance))
        {
            enemyOffset += Vector3.right + Vector3.forward;
        }
        else if ((Physics.Raycast(right, Vector3.forward, out hit, detectionDistance)
            && Physics.Raycast(center, Vector3.forward, out hit, detectionDistance))
            || Physics.Raycast(right, Vector3.forward, out hit, detectionDistance))
        {
            enemyOffset -= Vector3.right + Vector3.forward;
        }
        if (enemyOffset != Vector3.zero)
        {      
            Quaternion newRotation = Quaternion.Euler(0, 0, -45*enemyOffset.x);
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, transform.position + enemyOffset * (movementSpeed/10), Time.deltaTime);
        }
    }
}
