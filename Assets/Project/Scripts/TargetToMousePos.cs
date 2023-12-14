using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetToMousePos : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                target.transform.position = raycastHit.point;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GameObject.Find("Treasure").transform.position, 5f);
    }
}
