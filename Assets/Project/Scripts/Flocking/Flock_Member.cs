using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock_Member : MonoBehaviour
{
    public FlockManager flockManager;

    float currentTime = 0.0f;
    
    [SerializeField] float speed = 1.0f;
    Vector3 direction;
    public bool debugBoolisOutside = false;

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > flockManager.calculationSleep)
        {
            currentTime -= flockManager.calculationSleep;
            calculateFlocking();
        }


        //If outside limits
        if (transform.position.x < -flockManager.swimLimits.x || transform.position.x > flockManager.swimLimits.x ||
            transform.position.y < -flockManager.swimLimits.y + flockManager.transform.position.y || transform.position.y > flockManager.swimLimits.y + flockManager.transform.position.y ||
            transform.position.z < -flockManager.swimLimits.z || transform.position.z > flockManager.swimLimits.z)
        {
            debugBoolisOutside = true;

            Transform t = this.transform;
            t.LookAt(flockManager.transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(t.rotation.eulerAngles), flockManager.rotationSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction), flockManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            debugBoolisOutside = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), flockManager.rotationSpeed * Time.deltaTime);
        }

        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
    void calculateFlocking()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;



        int neighbourAmountInReach = 0;

        foreach (GameObject go in flockManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);

                if (distance <= flockManager.maxDistanceToFindNeighbours)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<Flock_Member>().direction;
                    separation -= (transform.position - go.transform.position) / (distance * distance);

                    neighbourAmountInReach++;
                }
            }
        }
        if (neighbourAmountInReach > 0)
        {
            cohesion = (cohesion / neighbourAmountInReach - transform.position).normalized * speed;
            align /= neighbourAmountInReach;
            speed = Mathf.Clamp(align.magnitude, flockManager.minSpeed, flockManager.maxSpeed);
        }




        direction = (cohesion + align + separation).normalized * speed;

        direction += new Vector3(Random.Range(-flockManager.randomFactor.x, flockManager.randomFactor.x),
                                 Random.Range(-flockManager.randomFactor.y, flockManager.randomFactor.y),
                                 Random.Range(-flockManager.randomFactor.z, flockManager.randomFactor.z));
    }
}
