using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool drawSpawnSphere = false;
    [SerializeField] bool drawLimitBox = false;
    [SerializeField] bool drawLeader = false;

    [Header("Spawn Parameters")]
    public float calculationSleep = 1.5f;

    [SerializeField] int numFish = 10;
    [SerializeField] float spawnSpread = 2.0f;
    [SerializeField] GameObject fishPrefab;
    public GameObject[] fishArray;

    [Header("Leader Parameters")]
    public GameObject leader;
    public bool followLeader = false;
    public bool leaderAutoMove = false;
    public float leaderStrength = 1.0f;

    [Header("Flocking Parameters")]
    public Vector3 swimLimits = Vector3.one;

    [Space (10)]

    [Range(0.0f, 50.0f)]
    public float maxDistanceToFindNeighbours;

    [Range (0.0f,10.0f)]
    [SerializeField] public float maxSpeed;
        
    [Range (0.0f,10.0f)]
    [SerializeField] public float minSpeed;

    [Range(0.0f, 3.0f)]
    [SerializeField] public float rotationSpeed;

    [Space(10)]

    public Vector3 randomFactorVariation = Vector3.one;

    void Start()
    {
        transform.position = new Vector3(0, swimLimits.y, 0);

        fishArray = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 position = this.transform.position + Random.insideUnitSphere * spawnSpread;
            Vector3 direction = Random.insideUnitSphere; // random vector direction
            fishArray[i] = (GameObject)Instantiate(fishPrefab, position, Quaternion.LookRotation(direction), this.transform);
            fishArray[i].GetComponent<Flock_Member>().flockManager = this;
        }
    }

    float time = 0;
    private void Update()
    {
        if (leaderAutoMove)
        {
            time += Time.deltaTime;
            if (time >= 360.0f)
            {
                time -= 360.0f;
            }

            Vector3 pos = Vector3.zero;
            float rad = time * Mathf.Deg2Rad * 50.0f;

            pos.x = Mathf.Cos(rad) * 2 + transform.position.x;
            pos.y = transform.position.y;
            pos.z = Mathf.Sin(rad) * 2 + transform.position.z;

            leader.transform.position = pos;
        }
    }

    private void OnDrawGizmos()
    {
        if (drawSpawnSphere)    Gizmos.DrawWireSphere(this.transform.position, spawnSpread);
        if (drawLimitBox)       Gizmos.DrawWireCube(this.transform.position, swimLimits * 2);
        if (drawLeader)         Gizmos.DrawSphere(leader.transform.position, 0.5f);
    }
}
