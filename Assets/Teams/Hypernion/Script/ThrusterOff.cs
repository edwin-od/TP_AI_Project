using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion
{
    public class ThrusterOff : Action
    {
        private HyperionTeamController hyperion;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionTeamController>();
            hyperion.thrust = 0f;
        }

    }
}
