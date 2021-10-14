using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Hyperion;
using DoNotModify;

public class FindWaypointTask : Action
{

    private HyperionController hyperion;
    public BehaviorTree behaviourTree;

    public override void OnStart() {
        hyperion = gameObject.GetComponent<HyperionController>();

        SpaceShipView spaceship = hyperion.mySpaceship;
        SpaceShipView otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - spaceship.Owner);
        Vector2 closestPos = otherSpaceship.Position;
        for (int i = 0; i < hyperion.currentData.WayPoints.Count; i++) {
            if ((hyperion.currentData.WayPoints[i].Owner == otherSpaceship.Owner || hyperion.currentData.WayPoints[i].Owner == -1)) {
                bool ignore = false;
                Vector2 shipToWaypoint = new Vector2(hyperion.currentData.WayPoints[i].Position.x - spaceship.Position.x, hyperion.currentData.WayPoints[i].Position.y - spaceship.Position.y);
                Debug.DrawRay(spaceship.Position, shipToWaypoint, Color.red);
                RaycastHit2D[] hits = Physics2D.RaycastAll(spaceship.Position, shipToWaypoint.normalized, shipToWaypoint.magnitude);
                for (int k = 0; k < hits.Length; k++) {
                    if (hits[k].collider) {
                        if (hits[k].transform.tag == "Asteroid")
                            ignore = true;
                    }
                }

                if (!ignore) {
                    if (closestPos == Vector2.zero) {
                        closestPos = hyperion.currentData.WayPoints[i].Position;
                        continue;
                    }

                    float angle = Vector2.SignedAngle(spaceship.Velocity, hyperion.currentData.WayPoints[i].Position);
                    angle = hyperion.NormaliseValue(angle);

                    //float offsetMultiplier = 180;
                    //float angleOffset = offsetMultiplier / spaceship.Velocity.magnitude ; //*
                    float angleOffset = 70; //*

                    if (spaceship.Owner != hyperion.currentData.WayPoints[i].Owner && Vector2.Distance(spaceship.Position, closestPos) > Vector2.Distance(spaceship.Position, hyperion.currentData.WayPoints[i].Position) && (angle < angleOffset || angle > 360 - angleOffset))
                    {
                        if (!hyperion.isBlocked || (hyperion.isBlocked && (Vector2)behaviourTree.GetVariable("TargetPos").GetValue() != hyperion.currentData.WayPoints[i].Position))
                        {
                            closestPos = hyperion.currentData.WayPoints[i].Position;
                        }
                        
                    }
                }
            }
        }
        hyperion.isBlocked = false;
        behaviourTree.SetVariableValue("TargetPos", closestPos);
    }



}
