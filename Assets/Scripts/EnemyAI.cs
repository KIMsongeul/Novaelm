using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private float detectionRange = 8f;
    private float wanderRadius = 5f;
    private float wanderTime = 4f;

    private NavMeshAgent agent;
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        timer = wanderTime;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
            Debug.Log("ChasePlayer");
        }
        else
        {
            Wander();
            Debug.Log("Wander");
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTime)
        {
            Vector3 newPos = RandomNavSphere(wanderRadius);
            agent.SetDestination(newPos);
            timer = 0;
            
            Debug.Log("New wander position set :" + newPos);
        }
    }

    private Vector3 RandomNavSphere(float dist)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection = transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, NavMesh.AllAreas);
        return navHit.position;
    }
    
}
