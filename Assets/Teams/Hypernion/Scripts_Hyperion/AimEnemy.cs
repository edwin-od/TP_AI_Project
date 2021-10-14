using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion1
{
    public class AimEnemy : Action
    {
        private HyperionController hyperion;
        public BehaviorTree behaviourTree;
        SpaceShipView otherSpaceship;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.mySpaceship.Owner);

            Vector2 diff = otherSpaceship.Position - hyperion.mySpaceship.Position;
            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            behaviourTree.SetVariableValue("Rotation", rot);
        }

    }
}
