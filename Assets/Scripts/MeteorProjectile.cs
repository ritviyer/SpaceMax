using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    float speed;

    public GameObject impactPrefab;
    public List<GameObject> trails;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        if (speed != 0 && rb != null)
        {
            rb.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    public void SetSpeed(float meteorSpeed)
    {
        speed = meteorSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        ////Inpactprefab instantiate at pos with rot rotation

        if (trails.Count > 0)
        {
            for(int i = 0; i<trails.Count; i++)
            {
                trails[i].transform.parent = null;
                var ps = trails[i].GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }

        //Instantiate Explosion
        Destroy(gameObject);
    }
}
