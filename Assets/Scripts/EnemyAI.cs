using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private float detectionRange = 10f;
    private float wanderRadius = 5f;
    private float wanderTime = 12f;

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
        }
        else
        {
            Wander();
        }
        timer += Time.deltaTime;
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void Wander()
    {
        if (timer >= wanderTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position,wanderRadius,3);
            agent.SetDestination(newPos);
            timer = 0;
            Debug.Log("New wander position set :" + newPos);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randomDirection = Random.insideUnitSphere * dist;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition (randomDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    
}
