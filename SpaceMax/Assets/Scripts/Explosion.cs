using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    [SerializeField] GameObject blowUp;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Shield shield;
    float laserHitModifier = 50f;

    void Explode(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform);
        if (!shield)
            return;
        shield.TakeDamage();
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            Explode(contact.point);
        }
    }

    public void AddForce(Vector3 hitPosition, Transform hitSource)
    {
        Explode(hitPosition);
        if (rigidbody == null)
            return;
        Vector3 direction = (hitSource.position - hitPosition).normalized;
        rigidbody.AddForceAtPosition(direction * laserHitModifier, hitPosition, ForceMode.Acceleration);
    }

    public void BlowUp()
    {
        Instantiate(blowUp, transform.position, Quaternion.identity);
        if (gameObject.CompareTag("Player"))
            EventManager.PlayerDeath();
        Destroy(gameObject);
    }
}
