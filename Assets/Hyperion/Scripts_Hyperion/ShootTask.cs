using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;

namespace Hyperion
{
    public class ShootTask : Action
    {
        private HyperionController hyperion;
        public BehaviorTree behaviourTree;
        public SharedVector2 Player;
        SpaceShipView otherSpaceship;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.mySpaceship.Owner);
            hyperion.needShoot = AimingHelpers.CanHit(hyperion.mySpaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.5f);
        }

    }
}
