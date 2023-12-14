using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Linq;

[Action("Cops/FindClosestCop")]
[Help("Get the Closest Free Cop.")]
public class BBFindClosestCop : BasePrimitiveAction
{
    [OutParam("game object")]
    [Help("Nearest free cop.")]
    public GameObject go;

    public override TaskStatus OnUpdate()
    {
        var l = GameObject.FindGameObjectsWithTag("Cop").Where(x => !x.GetComponent<TacticsMoves>().found);
        if (l.Count() == 0)
            return TaskStatus.FAILED;
        go = l.First();
        return TaskStatus.COMPLETED;
    }
}

