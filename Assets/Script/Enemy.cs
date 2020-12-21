using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{

    NavMeshAgent nav;
    Animator anim;
    [SerializeField]
    float followingDistance;
    float Distance;
    [SerializeField]
    float WaitForAttack = 3f;
    bool canAttack = true;
    bool attack2 = false;
    CharacterStats stats;
    [SerializeField]
    float WaitForAttack2 = 6f;
    bool Die = false;
   

    void Start()
    {
		//initialize some objects ^^
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
    
    }

    
    void Update()
    {
       
		Distance = Vector3.Distance(LevelManager.level.player.position, transform.position);//Distance btw player and enemy
        anim.SetFloat("Walk",nav.velocity.magnitude);
        if (Distance < followingDistance && stats.currentHealth > 0) {//Check if player near to enemy
        nav.SetDestination(LevelManager.level.player.position);
            if (Distance < nav.stoppingDistance &&  ! Movement.move.End())
            {
                if (canAttack) { 
                anim.SetTrigger("Attack");
                    Distance = 0;
                StartCoroutine(waitFor());
         
                }
                else if ( !canAttack && ! attack2)
                {
                    anim.SetTrigger("Attack2");
                    StartCoroutine(forAttack2());
                   
                }
            }
        }

    }
    IEnumerator forAttack2()
    {
        if (!Movement.move.GetComponent<CharacterStats>().over)
            attack2 = true;
        yield return new WaitForSeconds(WaitForAttack2);
         stats.power = 10;
        anim.SetTrigger("Attack2");
        attack2 = false;
    }
    void End()
    {
		//Kill enemy
        if (stats.currentHealth <= 0 && !Die)
        {
            LevelManager.level.EnemyKill++;
            LevelManager.level.Score += 200;
            anim.SetTrigger("Die");
            Instantiate(LevelManager.level.particles[2],  transform.position, transform.rotation);
            nav.SetDestination(transform.position);
            Die = true;
            Destroy(gameObject,3);
            Load.loab.Next();
        }
    }



    IEnumerator waitFor()
    {
		//Wait WaitForAttack(variable) to Attack
        canAttack = false;
        yield return new WaitForSeconds(WaitForAttack);
         stats.power = 5;
        if(!Movement.move.GetComponent<CharacterStats>().over)
        canAttack = true;

    }
 
    void OnTriggerEnter(Collider box)
    {
      //deacrease enemy health
        if (box.CompareTag("Player")) {
    
            stats.changeHealth(- box.GetComponentInParent<CharacterStats>().power * ((LevelManager.level.levelItem == 3) ? 5 : 1));
   
            End();

           } 
           
             

        
    }
    public void playerDamage()
    {
		//deacrease player health
        LevelManager.level.player.GetComponent<CharacterStats>().changeHealth(-stats.power );
        Movement.move.End();
         
    }
}
