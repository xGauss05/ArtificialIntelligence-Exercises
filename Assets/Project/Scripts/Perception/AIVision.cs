using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIVision : MonoBehaviour {
    public Camera frustum;
    public LayerMask mask;

    [SerializeField] NavMeshAgent agent;
    
    private bool foundTarget = false;
    [SerializeField] Vector3 localTarget;
    [SerializeField] Vector3 worldTarget;
    [SerializeField] int amountCalculations = 10;
    [SerializeField] float radius = 5.0f;
    [SerializeField] float offset = 3.0f;

    void checkEnmities() {
        Debug.Log("Checking enmities");
        Collider[] colliders = Physics.OverlapSphere(transform.position,frustum.farClipPlane,mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders) {
            
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes,col.bounds)) {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray,out hit,frustum.farClipPlane,mask)) {
                    if (hit.collider.gameObject.CompareTag("Peasant")) {
                        Debug.Log("Peasant found");
                        foundTarget = true;
                    }
                }
            }
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

    void Update() {
        Debug.Log("AIVision update");
        if (foundTarget) {

        } else {
            if (!agent.pathPending && agent.remainingDistance < 0.3f) {
                Wander();
            }
        }
    }

}
