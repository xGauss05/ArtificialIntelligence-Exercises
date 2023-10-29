using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    [SerializeField] GameObject[] hidingSpots;
    [SerializeField] NavMeshAgent agent;
    GameObject hidingSpot;
    bool hasStolen = false;


    public void Hide()
    {
        if (!hasStolen)
        {
            hasStolen = true;

            System.Func<GameObject, float> distance =
                                    (hs) => Vector3.Distance(transform.gameObject.GetComponent<BlackBoard>().cop.transform.position,
                                     hs.transform.position);
            hidingSpot = hidingSpots.Select(
                                        ho => (distance(ho), ho)
                                        ).Min().Item2;
            Debug.Log("Hiding behind " + hidingSpot.name);
        }


        Vector3 dir = hidingSpot.transform.position - transform.gameObject.GetComponent<BlackBoard>().cop.transform.position;

        Vector3 finalPos = hidingSpot.transform.position + dir.normalized;

        Seek(finalPos);
    }
    public void Seek(Vector3 target)
    {
        agent.destination = target;
    }

    float currentTime = 0.0f;
    public void Wander()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 0.5f)
        {
            currentTime -= 0.5f;

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
}
