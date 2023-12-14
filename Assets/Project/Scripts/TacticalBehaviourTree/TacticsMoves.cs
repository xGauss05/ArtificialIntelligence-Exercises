using UnityEngine;
using UnityEngine.AI;

public class TacticsMoves : MonoBehaviour
{
    GameObject target;
    public Collider floor;
    public NavMeshAgent agent;
    public GameObject robber;
    public GameObject treasure;
    [HideInInspector] public bool found = false;

    void Start()
    {
        TacticsWander();
    }

    void Update()
    {
        if (found)
            agent.destination = target.transform.position;
        if (!found && agent.remainingDistance < 2)
            TacticsWander();
    }

    public void BBSeekRobber() 
    {
        target = robber;
        found = true;
    }

    public void BBSeekTreasure() 
    {
        target = treasure;
        found = true;
    }

    public void TacticsWander()
    {
        float radius = 5;
        float distance = 7;

        Vector3 target = Vector3.zero;

        target += new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        target.Normalize();
        target *= radius;

        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(target + 
                                                new Vector3(0, 0, distance));

        if (!floor.bounds.Contains(targetWorld))
        {
            targetWorld = -transform.position * 0.1f;
        };

        agent.destination = targetWorld;
    }
}
