using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public int meleeAmount;
    public GameObject meleePrefab;
    public int rangedAmount;
    public GameObject rangedPrefab;
    public GameObject leader;

    void Start()
    {
        int front = 2 * meleeAmount / 3;
        int rear = meleeAmount - front;
        createRow(front, -2f, meleePrefab);
        createRow(rangedAmount, -4f, rangedPrefab);
        createRow(rear, -6f, meleePrefab);
    }

    void createRow(int num, float z, GameObject prefab)
    {
        float pos = 1 - num;
        for (int i = 0; i < num; ++i)
        {
            Vector3 position = leader.transform.TransformPoint(new Vector3(pos, 0f, z));
            GameObject temp = Instantiate(prefab, position, leader.transform.rotation);
            temp.AddComponent<Formation>();
            temp.GetComponent<Formation>().pos = new Vector3(pos, 0, z);
            temp.GetComponent<Formation>().target = leader;
            pos += 2f;
        }
    }
}
