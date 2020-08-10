using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enraged_attack : StateMachineBehaviour
{
    //public float speed = 1f;
    //public float attackRange = 10f;

    Transform player;
    Rigidbody2D rb;
    //Boss boss;

    public GameObject[] players;
    public Transform firePoint;
    public GameObject bulletPrefab;

    float fireRate;
    float nextFire;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        firePoint = animator.gameObject.transform.GetChild(0).GetComponent<Transform>();
        fireRate = 1f;
        nextFire = Time.time;
    }


    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody2D>().velocity = (player.transform.position - firePoint.position).normalized * 2f;

                nextFire = Time.time + fireRate;
            }
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
  
        CheckIfTimeToFire();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
