using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Patrolling :MonoBehaviour {

    [SerializeField] bool drawGizmos = false;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshAgent ghost;
    [SerializeField] GameObject[] waypoints;
    [SerializeField] bool backwards;
    int patrolWP = 0;


    void Start() {
        patrolWP = Random.Range(0,waypoints.Length - 1);

        int alea = Random.Range(0,2);
        if (alea % 2 == 0) {
            backwards = true;
        } else {
            backwards = false;
        }
    }

    void Update() {
        if (!ghost.pathPending && ghost.remainingDistance < 0.3f) Patrol();

        agent.destination = ghost.gameObject.transform.position;
    }

    void Patrol() {
        if (waypoints.Length == 0)
            return;

        ghost.destination = waypoints[patrolWP].transform.position;
        if (!backwards) {
            patrolWP = (patrolWP + 1) % waypoints.Length;
        } else {
            patrolWP--;
            if (patrolWP < 0) {
                patrolWP = waypoints.Length - 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.DrawSphere(ghost.transform.position, 1);
        }
    }
}
