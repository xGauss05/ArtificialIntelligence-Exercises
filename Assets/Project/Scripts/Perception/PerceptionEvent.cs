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
    private bool targetFound = false;
    void Start() {
        zombieArray = new GameObject[numZombies];

        for (int i = 0; i < numZombies; ++i) {

            Vector3 position = this.transform.position + Random.insideUnitSphere * spawnSpread;
            position.y = this.transform.position.y;
            Vector3 direction = Random.insideUnitSphere; // random vector direction
            direction.y = 0;
            zombieArray[i] = (GameObject)Instantiate(zombiePrefab,position,Quaternion.LookRotation(direction),this.transform);
        }
    }
    void ChaseTarget(GameObject target) {
        gameObject.BroadcastMessage("SwitchTarget",target);
    }
    // Update is called once per frame
    void Update() {
        Debug.Log("Perception event");
        if (!targetFound)
            gameObject.BroadcastMessage("checkEnmities");
    }
}
