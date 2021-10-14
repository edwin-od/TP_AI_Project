using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Hyperion;

public class TaskGoTo : Action
{
    public BehaviorTree behaviourTree;
    public SharedVector2 target;
    public SharedVector2 Player;
    public SharedFloat Orientation;

    public override void OnStart()
    {

    }
    public override TaskStatus OnUpdate()
    {

        //float diff = Vector2.Angle(Player.Value, target.Value);
        Vector2 diff = target.Value - Player.Value;
        float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        behaviourTree.SetVariableValue("Rotation", rot);

        return TaskStatus.Running;
    }

}
