using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Wandering : MonoBehaviour
{
    [SerializeField] bool drawGizmos = false;

    [SerializeField] float radius = 5.0f;
    [SerializeField] float offset = 3.0f;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 localTarget;
    [SerializeField] Vector3 worldTarget;

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            NavMeshHit hit;

            if (!NavMesh.SamplePosition(worldTarget, out hit, 0.5f, NavMesh.AllAreas))
            {
                calculateDestination();
                return;
            }

            Wander();
        }
    }

    private void calculateDestination()
    {
        localTarget = Random.insideUnitSphere * radius;
        localTarget.y = 0;
        localTarget += new Vector3(0, 0, offset);

        worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
    }
    private void Wander()
    {
        calculateDestination();
        agent.destination = worldTarget;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.DrawSphere(worldTarget, 1);
            Gizmos.DrawWireSphere(transform.position + transform.forward * offset, radius);
        }
    }
}
