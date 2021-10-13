using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime;

namespace Hyperion
{

    public class HyperionController : BaseSpaceShipController
    {
        public float thrust;
        public float targetOrient;
        public bool needShoot = false;
        public bool needMine = false;
        public bool needShockwawe = false;
        public SharedFloat Orientation = 0f;
        public bool PointSet = false;
        public bool shipDrifting = false;
        public float detectionRange;
        public GameData currentData;

        public BehaviorTree behaviourTree;

        public override void Initialize(SpaceShipView spaceship, GameData data)
        {

        }

        public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
        {
            currentData = data;
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
            if (!PointSet)
            {
                GetClosestPoint(spaceship, data);
            }
            behaviourTree.SetVariableValue("EnergyLevel", spaceship.Energy);
            behaviourTree.SetVariableValue("Score", (float)spaceship.Score);
            //behaviourTree.SetVariableValue("EnemyPosition",  otherSpaceship.Position);
            behaviourTree.SetVariableValue("PlayerPosition", spaceship.Position);

            Vector2 closestPos = Vector2.zero;
            for (int i = 0; i < data.WayPoints.Count; i++)
            {
                if ((data.WayPoints[i].Owner == otherSpaceship.Owner || data.WayPoints[i].Owner == -1))
                {
                    bool ignore = false;
                    Vector2 shipToWaypoint = new Vector2(data.WayPoints[i].Position.x - spaceship.Position.x, data.WayPoints[i].Position.y - spaceship.Position.y);
                    Debug.DrawRay(spaceship.Position, shipToWaypoint, Color.red);
                    RaycastHit2D[] hits = Physics2D.RaycastAll(spaceship.Position, shipToWaypoint.normalized, shipToWaypoint.magnitude);
                    for (int k = 0; k < hits.Length; k++)
                    {
                        if (hits[k].collider)
                        {
                            Debug.Log(hits[k].transform.gameObject.name);
                            if (hits[k].transform.tag == "Asteroid")
                                ignore = true;
                        }
                    }

                    if (!ignore)
                    {
                        if (closestPos == Vector2.zero)
                        {
                            closestPos = data.WayPoints[i].Position;
                            continue;
                        }

                        float angle = Vector2.SignedAngle(spaceship.Position, data.WayPoints[i].Position);
                        angle = NormaliseValue(angle);

                        if (Vector2.Distance(spaceship.Position, closestPos) > Vector2.Distance(spaceship.Position, data.WayPoints[i].Position))// && (angle < 10 || angle > 350))
                        {
                            closestPos = data.WayPoints[i].Position;
                        }
                    }
                }
            }

            behaviourTree.SetVariableValue("TargetPos", closestPos);

            Orientation = (SharedFloat)behaviourTree.GetVariable("Rotation");
            targetOrient = NormaliseValue(Orientation.Value);

            float highBound = NormaliseValue(targetOrient + detectionRange);
            float lowBound = NormaliseValue(targetOrient - detectionRange);

            thrust = 1f;

            //Debug.Log("cpcococol : " + Vector2.Distance(spaceship.Position, closestPos));
            //if(Vector2.Distance(spaceship.Position, closestPos) < 2f)
            //{
            //    thrust = 0.3f;
            //}
            //else 
            //{
            //    thrust = 1f;
            //}

            //if (spaceship.Orientation < highBound && spaceship.Orientation > lowBound)
            //{
            //     thrust = 1f;

            //}else
            //{
            //    thrust = 1f;
            //}

            needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
            InputData input = new InputData(thrust, targetOrient, needShoot, needMine, needShockwawe);
            needMine = false;
            needShockwawe = false;
            needShoot = false;

            return input;


        }

        public void GetClosestPoint(SpaceShipView spaceship, GameData data)
        {
            int selected = 0;
            float minDist = Mathf.Infinity;
            Vector2 currentPos = spaceship.Position;
            for (int i = 0; i < data.WayPoints.Count; i++)
            {
                float dist = Vector3.Distance(data.WayPoints[i].Position, currentPos);
                if (dist < minDist)
                {
                    selected = i;
                    minDist = dist;
                }

            }

            behaviourTree.SetVariableValue("ClosestPoint", data.WayPoints[selected].Position);
            PointSet = true;

        }


        public float NormaliseValue(float value)
        {
            if (value < 0)
                value += 360;
            else if (value > 360)
                value -= 360;

            return value;
        }

        //fonction commentary()
        //{
        //    float highBound = NormaliseValue(targetOrient + detectionRange);
        //    float lowBound = NormaliseValue(targetOrient - detectionRange);
        //    float velo = NormaliseValue(Mathf.Atan2(spaceship.Velocity.x, spaceship.Velocity.y) * Mathf.Rad2Deg);
        //    float veryHighBound = NormaliseValue(velo + 90);
        //    float veryLowBound = NormaliseValue(velo - 90);


        //    float slowVelocity = 0.2f;

        //    if (!needToHairpin && spaceship.Velocity.magnitude > slowVelocity && spaceship.Orientation > veryHighBound && spaceship.Orientation < veryLowBound)
        //    {
        //        Debug.Log("start demi tour");

        //        Vector2 diff = -spaceship.Velocity;
        //        float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //        targetOrient = NormaliseValue(rot);

        //        needToHairpin = true;
        //    }

        //    if (!needToHairpin)
        //    {
        //        Debug.Log("no demi tour : " + spaceship.Velocity.magnitude);

        //        if (spaceship.Orientation < highBound && spaceship.Orientation > lowBound)
        //        {
        //            thrust = 1f;
        //        }
        //        else
        //        {
        //            thrust = 0f;

        //        }

        //    }
        //    else
        //    {
        //        if (spaceship.Velocity.magnitude > slowVelocity)
        //        {
        //            Vector2 diff = -spaceship.Velocity;
        //            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //            float vel = NormaliseValue(rot);

        //            float velLowBound = NormaliseValue(vel - 10);
        //            float velHighBound = NormaliseValue(vel + 10);

        //            if (spaceship.Orientation < velHighBound && spaceship.Orientation > velLowBound)
        //            {
        //                Debug.Log("demi tour frein : acceleration+");
        //                thrust = 1f;
        //            }
        //            else
        //            {
        //                Debug.Log("demi tour frein : nope");

        //                thrust = 0f;
        //            }
        //            Debug.Log(velLowBound + "     " + vel + "     " + velHighBound);

        //        }
        //        else
        //        {
        //            Debug.Log("demi tour fini");
        //            thrust = 0f;
        //            needToHairpin = false;
        //        }
        //    }
        //}

    }



}
