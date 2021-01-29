using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            //call Pickup function
        }
    }
    void Pickup()
    {
        //call even that increases score
        //Destroy self
    }
}
