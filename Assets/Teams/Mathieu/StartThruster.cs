using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion
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
