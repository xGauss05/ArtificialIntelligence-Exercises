using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock_Member : MonoBehaviour
{
    public FlockManager flockManager;

    [SerializeField] float speed = 1.0f;
    Vector3 direction;

    void Update()
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

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), flockManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
}
