using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Transform asteroidT;
    Vector3 randomRotation;

    private float minScale;
    private float maxScale;
    private float rotationOffset;

    float randomMove;
    float moveSpeed;

    Vector3 direction = Vector3.zero;

    void Awake()
    {
        asteroidT = transform;
    }

    void Start()
    {
        GetInitialParameters();
        Vector3 scale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
        asteroidT.localScale = scale;
        randomRotation = new Vector3(Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset));
        if (Random.Range(0, 100) < randomMove)
            MoveAndroid();
    }

    void Update()
    {
        if (Camera.main.transform.position.z > asteroidT.position.z)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        asteroidT.Rotate(randomRotation * Time.deltaTime);
        if (direction != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + direction * moveSpeed, Time.deltaTime);
        }
    }
    void MoveAndroid() 
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
            direction = Vector3.up * -1;
        else if (rand == 1)
            direction = Vector3.right;
        else if (rand == 2)
            direction = Vector3.right * -1;
    }
    void GetInitialParameters()
    {
        Player player = FindObjectOfType<Player>();
        float pSpeed = player.GetSpeed();
        if (pSpeed == 100f)
        {
            minScale = 10f;
            maxScale = 15f;
            rotationOffset = 100f;
            randomMove = 10f;
            moveSpeed = 100f;
        }
        else if (pSpeed == 150f)
        {
            minScale = 10f;
            maxScale = 20f;
            rotationOffset = 112f;
            randomMove = 15f;
            moveSpeed = 112f;
        }
        else if (pSpeed == 200f)
        {
            minScale = 10f;
            maxScale = 20f;
            rotationOffset = 124f;
            randomMove = 20f;
            moveSpeed = 124f;
        }
        else if (pSpeed == 250f)
        {
            minScale = 15f;
            maxScale = 25f;
            rotationOffset = 138f;
            randomMove = 25f;
            moveSpeed = 138f;
        }
        else if (pSpeed == 300f)
        {
            minScale = 15f;
            maxScale = 25f;
            rotationOffset = 150f;
            randomMove = 30f;
            moveSpeed = 150f;
        }
        else if (pSpeed == 350f)
        {
            minScale = 15f;
            maxScale = 25f;
            rotationOffset = 162f;
            randomMove = 35f;
            moveSpeed = 162f;
        }
        else if (pSpeed == 400f)
        {
            minScale = 15f;
            maxScale = 25f;
            rotationOffset = 174f;
            randomMove = 40f;
            moveSpeed = 174f;
        }
        else if (pSpeed == 450f)
        {
            minScale = 20f;
            maxScale = 30f;
            rotationOffset = 187f;
            randomMove = 45f;
            moveSpeed = 187f;
        }
        else if (pSpeed == 500f)
        {
            minScale = 20f;
            maxScale = 35f;
            rotationOffset = 200f;
            randomMove = 50f;
            moveSpeed = 200f;
        }
        else
        {
            minScale = 15f;
            maxScale = 25f;
            rotationOffset = 124f;
            randomMove = 20f;
            moveSpeed = 124f;
        }
    }
}
