using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion
{
    public class GetHitEnemy : Action
    {
        private HyperionController hyperion;
        SpaceShipView otherSpaceship;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.mySpaceship.Owner);
            
        }

        public override TaskStatus OnUpdate()
        {
            if (otherSpaceship.HitCount > 0)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}
