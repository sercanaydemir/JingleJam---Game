using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public abstract class enemystats : MonoBehaviour, IDamagable,IHealth
{
    [Header("enemy settings")]
    public float speed = 3;
    public float attackrange = 1.4f;
    public float attackrate = 1;
    public float turnspeed = 50f;

    protected void Awake()
    {
        Health = 100;
    }

    protected void OnEnable()
    {
        EventManager.OnDamage += Damage;
    }
    protected void OnDisable()
    {
        EventManager.OnDamage -= Damage;
    }

    public void Damage(IDamagable damagable,float value)
    {
        if (ReferenceEquals(damagable, this))
        {
            ChangeHealth(Health);
        }
    }

    public float Health { get; set; }
    public void ChangeHealth(float value)
    {
        Health -= value;
        if(Health == 0)
            Death();
    }
    
    protected abstract void Death();
}
public class enemycontroller : enemystats
{
    NavMeshAgent enemynav;
    Animator enemyanim;
    GameObject player;
    private enemyhealth _enemyhealth;
    float i = 5f;
    private float currentattacktime = 1f;
    
    private void Awake()
    {
        _enemyhealth = GetComponent<enemyhealth>();
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        enemynav = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemynav.destination = player.transform.position;
        enemyanim = GetComponent<Animator>();
        enemynav.stoppingDistance = attackrange;
        enemyanim.SetTrigger("walk");
    }

    protected override void Death()
    {
        _enemyhealth.enemydeath();
    }

    void Update()
    {
        enemywalkandrun();
    }
    void enemywalkandrun()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        // if (player.GetComponent<playerhealth>().currenthealth <= 0)
        // {
        //     // if player die,
        //     enemynav.isStopped = true;
        //     enemynav.speed = 0;
        //     enemyanim.ResetTrigger("attack");
        //     enemyanim.ResetTrigger("walk");
        //     enemyanim.SetTrigger("idle");
        //     i -= Time.deltaTime;
        //     if (i <= 0)
        //     {
        //         Destroy(gameObject);
        //     }
        //
        // }
        // else if (player.GetComponent<playerhealth>().currenthealth > 0)
        // {
        //     
        // }
        
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
