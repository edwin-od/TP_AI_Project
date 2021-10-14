using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion
{
    public class DropMine : Action
    {

        public override void OnStart()
        {
            gameObject.GetComponent<HyperionTeamController>().needMine = true;
        }
    }

}


