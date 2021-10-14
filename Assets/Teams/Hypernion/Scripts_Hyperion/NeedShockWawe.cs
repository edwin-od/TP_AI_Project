using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion1
{
    public class NeedShockWawe : Action
    {
        private HyperionController hyperion;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            hyperion.needShockwawe = true;
        }


    }
}
