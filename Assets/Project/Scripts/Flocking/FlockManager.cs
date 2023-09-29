using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [SerializeField] int numFish = 10;
    [SerializeField] float spawnSpread = 2.0f;
    [SerializeField] GameObject fishPrefab;

    public GameObject[] allFish;

    [Range(0.0f, 50.0f)]
    public float maxDistanceToFindNeighbours;

    [Range (0.0f,10.0f)]
    [SerializeField] public float maxSpeed;
        
    [Range (0.0f,10.0f)]
    [SerializeField] public float minSpeed;

    [Range(0.0f, 10.0f)]
    [SerializeField] public float rotationSpeed;

    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 position = this.transform.position + Random.insideUnitSphere * spawnSpread;
            Vector3 direction = Random.insideUnitSphere; // random vector direction
            allFish[i] = (GameObject)Instantiate(fishPrefab, position, Quaternion.LookRotation(direction), this.transform);
            allFish[i].GetComponent<Flock_Member>().flockManager = this;
        }
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, spawnSpread);
    }
}
