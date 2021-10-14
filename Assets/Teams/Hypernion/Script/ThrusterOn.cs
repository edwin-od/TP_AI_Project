using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion
{
    public class ThrusterOn : Action
    {
        private HyperionTeamController hyperion;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionTeamController>();
            hyperion.thrust = 1f;
        }
    }
}
