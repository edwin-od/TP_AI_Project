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

    public override TaskStatus OnUpdate() 
    {
        float rotation = Vector2.SignedAngle(Player.Value, target.Value);
        return TaskStatus.Running;
    }

}
