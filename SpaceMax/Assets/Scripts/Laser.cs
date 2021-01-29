using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    LineRenderer lr;
    Light laserLight;
    [SerializeField]GameObject rigidbody;

    bool canFire;
    float laserOffTime = 0.03f;
    float maxD = 150f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }
    void Start()
    {
        lr.enabled = false;
        laserLight.enabled = false;
        canFire = true;
    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.position + maxDistance, Color.yellow);
    }

    public Vector3 Raycast(Vector3 startPosition, Vector3 targetPosition)
    {
        RaycastHit hit;
        targetPosition = targetPosition * maxD;
        //Debug.Log(targetPosition);
        Debug.DrawRay(startPosition, targetPosition, Color.black);
        if (Physics.Raycast(startPosition, targetPosition,out hit))
        {
            Debug.Log("We Hit " + hit.transform.name);
            SpawnExplosion(hit.point, hit.transform);
            EventManager.ScorePoints(10);
            return hit.point;
        }
        Debug.Log("We Missed");
        return startPosition + targetPosition;
    }
    void SpawnExplosion(Vector3 hitPosition, Transform target)
    {
        Explosion temp = target.GetComponent<Explosion>();
        if (temp != null)
        {
            temp.AddForce(hitPosition, transform);
        }
    }

    public void FireLaserPlayer(Vector3 targetPosition)
    {
        FireLaser(targetPosition);
    }

    public void FireLaser(Vector3 targetPosition, Transform target = null)
    {
        if (canFire)
        {
            if (target != null)
            {
                //SpawnExplosion(targetPosition, target);
            }
            if (rigidbody)
            {
                Vector3 distance = targetPosition - transform.position;
                float diff = distance.magnitude;
                Vector3 direction = distance / diff;
                direction.Normalize();

                GameObject b = Instantiate(rigidbody, transform.position, transform.rotation) as GameObject;
                Destroy(b, 3f);
                //b.transform.position = transform.position;
                //b.transform.rotation = transform.rotation;

                //change the player getting part later
                Player player = FindObjectOfType<Player>();
                float pSpeed = player.GetSpeed();
                //it was direction *200
                b.GetComponent<Rigidbody>().velocity = direction * (pSpeed + 100f);
            }
            else 
            { 
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, targetPosition);
                lr.enabled = true;
                laserLight.enabled = true;
                canFire = false;
                Invoke("TurnOffLaser", laserOffTime);
            }
        }
    }

    void TurnOffLaser()
    {
        lr.enabled = false;
        laserLight.enabled = false;
        canFire = true;
    }

    public float LaserDistance()
    {
        return maxD;
    }
}
