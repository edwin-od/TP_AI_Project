using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Hyperion;

public class ShootTask : Action
{
    private HyperionController hyperion;

    public override void OnStart()
    {
        hyperion = gameObject.GetComponent<HyperionController>();
        hyperion.needShoot = true;

    }
}
