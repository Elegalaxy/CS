using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    public aiWeapon weapon;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    float attackTime;
    bool isAttacked;

    //Detect
    public float sightRange, attackRange;
    public bool playerSightRange, playerAttackRange;
    public bool gotAttackedByPlayer;

    private void Awake() {
        gotAttackedByPlayer = false;
        player = GameObject.Find("Player").transform; //Get player
        agent = GetComponent<NavMeshAgent>(); //Get agent
        weapon = gameObject.transform.Find("Weapon").GetComponent<aiWeapon>();
        attackTime = 0.5f;
    }

    private void Update() {
        playerSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask); //Check if player in sight
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask); //Check if player in attack range

        if(!playerSightRange && !playerAttackRange && !gotAttackedByPlayer) patroling(); //If not in sight and attack range, patrol 
        if((playerSightRange && !playerAttackRange) || gotAttackedByPlayer) chase(); //If in sight but not attack range, chase
        if(playerSightRange && playerAttackRange) attack(); //If in sight and range, attack
    }

    void patroling() {
        if(!walkPointSet) searchWalkPoints(); //Search a new walk point if walk point is not set

        if(walkPointSet) agent.SetDestination(walkPoint); //If walk point is set, walk to it

        Vector3 distanceToWalkPoint = transform.position - walkPoint; //Distance before reach that point

        if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false; //If distance close enough, find new point
    }

    void searchWalkPoints() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange); //Random a point
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); //Set walk point
        if(Physics.Raycast(walkPoint, -transform.up, 2f, groundMask)) walkPointSet = true; //If the walk point is on a ground, walk point is set
    }

    void chase() {
        agent.SetDestination(player.position); //Walk to player position
    }
    
    void attack() {
        agent.SetDestination(transform.position); //Stop walking
        transform.LookAt(player); //Look at player

        if(!isAttacked) {
            //Attack
            weapon.Shoot();
            isAttacked = true;
            Invoke(nameof(resetAttack), attackTime); //Reset attack after time
        }
    }

    void resetAttack() {
        isAttacked = false; //Reset attack
    }
}
