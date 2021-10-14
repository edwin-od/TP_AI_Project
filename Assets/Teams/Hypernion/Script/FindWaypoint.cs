using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using DoNotModify;

namespace Hyperion
{
    public class FindWaypoint : Action
    {

        private HyperionTeamController hyperion;
        public BehaviorTree behaviourTree;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionTeamController>();

            SpaceShipView spaceship = hyperion.mySpaceship;
            SpaceShipView otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - spaceship.Owner);
            Vector2 closestPos = Vector2.zero;
            for (int i = 0; i < hyperion.currentData.WayPoints.Count; i++)
            {
                if (hyperion.currentData.WayPoints[i].Owner != hyperion.mySpaceship.Owner)
                {
                    if (closestPos == Vector2.zero)
                    {
                        closestPos = hyperion.currentData.WayPoints[i].Position;
                        continue;
                    }

                    float angle = Vector2.SignedAngle(spaceship.Velocity, hyperion.currentData.WayPoints[i].Position);
                    float angleOffset = 35;
                    if (spaceship.Owner != hyperion.currentData.WayPoints[i].Owner && Vector2.Distance(spaceship.Position, closestPos) > Vector2.Distance(spaceship.Position, hyperion.currentData.WayPoints[i].Position) && (angle < angleOffset || angle > -angleOffset))
                    {
                        if (!hyperion.isBlocked || (hyperion.isBlocked && (Vector2)behaviourTree.GetVariable("TargetPos").GetValue() != hyperion.currentData.WayPoints[i].Position))
                            closestPos = hyperion.currentData.WayPoints[i].Position;

                    }
                }
            }
            hyperion.isBlocked = false;
            behaviourTree.SetVariableValue("TargetPos", closestPos);
        }



    }

}