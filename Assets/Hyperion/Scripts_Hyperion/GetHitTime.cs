using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion
{
    public class GetHitTime : Action
    {
        private HyperionController hyperion;
        SpaceShipView otherSpaceship, currentSpaceship;
        public SharedVector2 ship;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.mySpaceship.Owner);
            if(ship.Name == "PlayerPosition")
            {
                currentSpaceship = hyperion.mySpaceship;
            }
            else
            {
                currentSpaceship = otherSpaceship;
            }
        }

        public override TaskStatus OnUpdate()
        {
            //Debug.Log("hitcount : " + currentSpaceship.HitCount);
            if (currentSpaceship.HitCount > 0)
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
