using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
public class enemyhealth : MonoBehaviour
{
    public float currenthealth;
    public float maxhealth = 10;
    Animator enemyanim;

    void Start()
    {
        currenthealth = maxhealth;
        enemyanim = GetComponent<Animator>();
    }

    public void enemydeath()
    {
        if (!enemyanim.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {

            enemyanim.SetBool("death", true);
            GetComponent<enemycontroller>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            enabled = false;
            
            Passive();
        }
    }

    void Passive()
    {
        Destroy(gameObject,3);
    }
    public void takedamage(float amount)
    {
        if (currenthealth > 0)
        {
            enemyanim.SetTrigger("hit");
            currenthealth -= amount;
        }
    }
    public void destroyobject()
    {
        enemyanim.ResetTrigger("hit");
        Destroy(gameObject, 1f);
    }
}
