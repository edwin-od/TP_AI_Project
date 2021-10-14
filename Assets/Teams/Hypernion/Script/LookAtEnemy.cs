using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion
{
    public class LookAtEnemy : Action
    {
        private HyperionTeamController hyperion;
        public BehaviorTree behaviourTree;
        SpaceShipView otherSpaceship;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionTeamController>();
            otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.mySpaceship.Owner);

            Vector2 diff = otherSpaceship.Position - hyperion.mySpaceship.Position;
            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            behaviourTree.SetVariableValue("Rotation", rot);
        }

    }
}
