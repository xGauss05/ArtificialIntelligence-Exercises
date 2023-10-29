using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    public float dist2Steal = 10f;
    public Transform cop;
    public GameObject treasure;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(treasure.transform.position, dist2Steal);
    }
}