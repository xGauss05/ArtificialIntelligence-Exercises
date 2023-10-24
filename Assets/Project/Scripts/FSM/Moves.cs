using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    GameObject[] hidingSpots;
    GameObject policeMan;
    Vector3 movement;
    float acceleration;
    Quaternion rotation;
    [SerializeField] NavMeshAgent agent;

    public void Hide()
    {
        System.Func<GameObject, float> distance =
                                    (hs) => Vector3.Distance(policeMan.transform.position,
                                     hs.transform.position);
        GameObject hidingSpot = hidingSpots.Select(
                                    ho => (distance(ho), ho)
                                    ).Min().Item2;
        Vector3 dir = hidingSpot.transform.position - policeMan.transform.position;
        Ray backRay = new Ray(hidingSpot.transform.position, -dir.normalized);
        RaycastHit info;
        hidingSpot.GetComponent<Collider>().Raycast(backRay, out info, 50f);

        Seek(info.point + dir.normalized);
    }
    public void Seek(Vector3 target)
    {
        agent.destination = target;
    }
    public void Wander()
    {
        Vector3 localTarget;
        Vector3 worldTarget;


        NavMeshHit hit;

        int count = 0;
        do
        {
            localTarget = Random.insideUnitSphere * 5;
            //localTarget.y = 0;
            localTarget += new Vector3(0, 0, 3);

            worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;
            count++;
            if (count >= 10) break;

        } while (!NavMesh.SamplePosition(worldTarget, out hit, 1.0f, NavMesh.AllAreas));

        agent.destination = worldTarget;
    }
}
