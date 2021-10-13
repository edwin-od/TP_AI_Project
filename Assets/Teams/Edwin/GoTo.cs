using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Hyperion;

public class GoTo : Action
{
    private HyperionController hyperion;

    public override void OnStart()
    {
        hyperion = gameObject.GetComponent<HyperionController>();
    }

    //public override void OnUpdate()
    //{
        
        
    //}
}