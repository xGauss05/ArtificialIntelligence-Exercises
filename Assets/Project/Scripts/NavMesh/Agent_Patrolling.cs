using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Patrolling : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject[] waypoints;
    int patrolWP = 0;

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f) Patrol();
    }

    private void Patrol()
    {
        patrolWP = (patrolWP + 1) % waypoints.Length;
        agent.destination = waypoints[patrolWP].transform.position;
    }
}
