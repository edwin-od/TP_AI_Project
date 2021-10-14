using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion1
{
    public class StartThruster : Action
    {
        private HyperionController hyperion;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            hyperion.thrust = 1f;
        }
    }
}
