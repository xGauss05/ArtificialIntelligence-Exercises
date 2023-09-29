using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Patrolling : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject[] waypoints;
    int patrolWP = 0;

    void Start() 
    {
        patrolWP = Random.Range(0,waypoints.Length - 1);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f) Patrol();
    }

    void Patrol()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[patrolWP].transform.position;
        patrolWP = (patrolWP + 1) % waypoints.Length;
        
    }
}
