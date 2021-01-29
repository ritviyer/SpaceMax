using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int currHealth;
    [SerializeField] float regenerationRate = 2f;
    [SerializeField] int regenerateAmount = 1;

    void Start()
    {
        currHealth = maxHealth;
        InvokeRepeating("Regenerate", regenerationRate, regenerationRate);
    }

    void Regenerate()
    {
        if (currHealth == maxHealth)
            CancelInvoke();
        else
        {
            currHealth += regenerateAmount;
            EventManager.HealthDamage(currHealth / (float)maxHealth);
        }
    }

    public void TakeDamage(int dmg = 1)
    {
        currHealth -= dmg;
        if (currHealth == 0)
        {
            GetComponent<Explosion>().BlowUp();
        }
        EventManager.HealthDamage(currHealth / (float)maxHealth);
    }
}
