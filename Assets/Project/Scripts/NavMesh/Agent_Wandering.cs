using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Wandering :MonoBehaviour {
    [SerializeField] bool drawGizmos = false;
    [SerializeField] int amountCalculations = 10;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float offset = 3.0f;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 localTarget;
    [SerializeField] Vector3 worldTarget;

    void Update() {
        if (!agent.pathPending && agent.remainingDistance < 0.3f) {
            Wander();
        }
    }

    private void calculateDestination() {
        NavMeshHit hit;

        int count = 0;
        do {
            localTarget = Random.insideUnitSphere * radius;
            //localTarget.y = 0;
            localTarget += new Vector3(0,0,offset);

            worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;
            count++;
            if (count >= amountCalculations) break;

        } while (!NavMesh.SamplePosition(worldTarget,out hit,1.0f,NavMesh.AllAreas));

        

    }

    private void Wander() {
        calculateDestination();
        agent.destination = worldTarget;
    }

    private void OnDrawGizmos() {
        if (drawGizmos) {
            Gizmos.DrawSphere(worldTarget,1);
            Gizmos.DrawWireSphere(transform.position + transform.forward * offset,radius);
        }
    }
}
