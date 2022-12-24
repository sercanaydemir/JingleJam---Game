using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class enemystats : MonoBehaviour
{
    [Header("enemy settings")]
    public float speed = 3;
    public float attackrange = 1.4f;
    public float attackrate = 1;
    public float turnspeed = 50f;
}
public class enemycontroller : enemystats
{
    NavMeshAgent enemynav;
    Animator enemyanim;
    GameObject player;
    float i = 5f;
    private float currentattacktime = 1f;

    private void Awake()
    {
        enemynav = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemynav.destination = player.transform.position;
        enemyanim = GetComponent<Animator>();
        enemynav.stoppingDistance = attackrange;
        enemyanim.SetTrigger("walk");
    }
    void Update()
    {
        enemywalkandrun();
    }
    void enemywalkandrun()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (player.GetComponent<playerhealth>().currenthealth <= 0)
        {
            // if player die,
            enemynav.isStopped = true;
            enemynav.speed = 0;
            enemyanim.ResetTrigger("attack");
            enemyanim.ResetTrigger("walk");
            enemyanim.SetTrigger("idle");
            i -= Time.deltaTime;
            if (i <= 0)
            {
                Destroy(gameObject);
            }

        }
        else if (player.GetComponent<playerhealth>().currenthealth > 0)
        {
            if (currentattacktime <= attackrate)
            {
                currentattacktime += Time.deltaTime;
            }
            if (distance <= attackrange)
            {
                // if player in attack range
                enemynav.isStopped = true;
                enemynav.speed = 0;
                enemyanim.ResetTrigger("walk");
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime * 5f);
                if (currentattacktime >= attackrate)
                {
                    enemyanim.SetTrigger("attack");
                    currentattacktime = 0f;
                }
            }
            else if (distance >= attackrange + 0.10f)
            {
                // if player not in attack range
                if (!enemyanim.IsInTransition(0) && !enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    enemynav.isStopped = false;
                    enemyanim.ResetTrigger("attack");
                    enemyanim.SetTrigger("walk");
                    enemynav.speed = speed;
                    enemynav.destination = player.transform.position;
                }

            }
        }
    }

}
