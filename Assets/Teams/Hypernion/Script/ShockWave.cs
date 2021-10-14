using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion
{
    public class ShockWave : Action
    {

        public override void OnStart()
        {
            gameObject.GetComponent<HyperionTeamController>().needShockwawe = true;
        }


    }
}
