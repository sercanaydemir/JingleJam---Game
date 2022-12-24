using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemysworddamage : MonoBehaviour
{
    public float damage = 5f;
    private void OnTriggerEnter(Collider other)
    {
        enemydamage(other, damage);
    }
    void enemydamage(Collider other,float damage)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<playerhealth>().currenthealth -= damage;
        }
    }
}
