using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Hyperion;


public class DropMinetask : Action
{
    private HyperionController hyperion;

    public override void OnStart()
    {
        hyperion = gameObject.GetComponent<HyperionController>();
        hyperion.needMine = true;
    }
}


