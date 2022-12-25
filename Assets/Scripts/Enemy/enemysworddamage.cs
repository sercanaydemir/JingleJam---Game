using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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
            IDamagable id = other.GetComponent<IDamagable>();
            if (id != null)
            {
                id.Damage(id,damage);
            }
        }
    }
}
