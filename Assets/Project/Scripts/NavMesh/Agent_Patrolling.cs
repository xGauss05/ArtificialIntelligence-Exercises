using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Patrolling :MonoBehaviour {
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject[] waypoints;
    int patrolWP = 0;
    bool backwards;
    void Start() {
        patrolWP = Random.Range(0,waypoints.Length - 1);

        int alea = Random.Range(1,2);
        if (alea % 2 == 0) {
            backwards = true;
        } else {
            backwards = false;
        }
    }

    void Update() {
        if (!agent.pathPending && agent.remainingDistance < 0.3f) Patrol();
    }

    void Patrol() {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[patrolWP].transform.position;
        if (!backwards) {
            patrolWP = (patrolWP + 1) % waypoints.Length;
        } else {
            patrolWP--;
            if (patrolWP < 0) {
                patrolWP = waypoints.Length - 1;
            }
        }


    }
}
