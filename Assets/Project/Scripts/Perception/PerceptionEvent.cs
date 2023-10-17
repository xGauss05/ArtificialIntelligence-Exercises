using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionEvent :MonoBehaviour {
    // Start is called before the first frame update
    [Header("Group parameters")]
    [SerializeField] uint numZombies = 5;
    [SerializeField] float spawnSpread = 2.0f;
    public GameObject[] zombieArray;
    [SerializeField] GameObject zombiePrefab;
    [Space(10)]

    [Header("Leader Parameters")]
    private GameObject leader;


    private AIVision aivision;

    void Start() {
        zombieArray = new GameObject[numZombies];

        for (int i = 0; i < numZombies; ++i) {
            
            Vector3 position = this.transform.position + Random.insideUnitSphere * spawnSpread;
            position.y = this.transform.position.y;
            Vector3 direction = Random.insideUnitSphere; // random vector direction
            direction.y = 0;
            zombieArray[i] = (GameObject)Instantiate(zombiePrefab,position,Quaternion.LookRotation(direction),this.transform);
        }

        leader = zombieArray[0];
    }

    // Update is called once per frame
    void Update() {

    }
}
