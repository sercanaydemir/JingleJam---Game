using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {
        enemydeath();
        
    }
    public void enemydeath()
    {
        if (currenthealth <= 0)
        {
            if (!enemyanim.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {

                enemyanim.SetBool("death", true);
                enemyanim.ResetTrigger("attack");
                enemyanim.ResetTrigger("walk");
                enemyanim.ResetTrigger("hit");

                GetComponent<enemycontroller>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                enabled = false;
            }
        }
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
