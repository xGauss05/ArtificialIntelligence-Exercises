using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Robber Near?")]
[Help("Checks whether Robber is near the Treasure.")]
public class IsRobberNear : ConditionBase
{
    public override bool Check()
    {
        GameObject robber = GameObject.Find("Tactics_Robber");
        GameObject treasure = GameObject.Find("Treasure");
        return Vector3.Distance(robber.transform.position, treasure.transform.position) < 5f;
    }
}