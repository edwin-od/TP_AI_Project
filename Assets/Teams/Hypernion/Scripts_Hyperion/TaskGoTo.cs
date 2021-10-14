using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion1
{
    public class TaskGoTo : Action
    {
        public BehaviorTree behaviourTree;
        private HyperionController hyperion;
        public SharedVector2 target;
        public SharedVector2 Player;
        public float count = 0;
        public float abandonTime = 4;
        // public float SlowDownRadiusMultiplier;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionController>();
            Vector2 diff = target.Value - Player.Value;
            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            behaviourTree.SetVariableValue("Rotation", rot);
            count = 0;
        }
        public override TaskStatus OnUpdate()
        {
            count += Time.deltaTime;
            Debug.Log("count : " + hyperion.mySpaceship.Owner + " || "  + count);
            if (count >= abandonTime)
            {
                hyperion.isBlocked = true;
                return TaskStatus.Failure;
            }

            if (Vector2.Distance(target.Value, Player.Value) > hyperion.currentData.WayPoints[0].Radius + hyperion.mySpaceship.Radius)
            {
                for (int i = 0; i < hyperion.currentData.WayPoints.Count; i++)
                {
                    if (hyperion.currentData.WayPoints[i].Position == target.Value)
                    {
                        if (hyperion.mySpaceship.Owner == hyperion.currentData.WayPoints[i].Owner)
                        {
                            Debug.Log("FAILEDWORLD");
                            return TaskStatus.Failure;
                        }
                    }
                }
            }
            if (hyperion && hyperion.currentData != null)
            {
                //Debug.Log("diff = " + hyperion.mySpaceship.Owner + " : " + Vector2.Distance(target.Value, Player.Value));
                if (Vector2.Distance(target.Value, Player.Value) < hyperion.currentData.WayPoints[0].Radius)
                {
                    Debug.Log("SUCCESS");
                    hyperion.thrust = 0.0f;
                    return TaskStatus.Success;
                }else
                {
                    hyperion.thrust = 1.0f;
                }

                //Vector2 shipToWaypoint = new Vector2(target.Value.x - Player.Value.x, target.Value.y - Player.Value.y);
                //Debug.DrawRay(hyperion.mySpaceship.Position, shipToWaypoint, Color.red);
                //RaycastHit2D[] hits = Physics2D.RaycastAll(Player.Value, shipToWaypoint.normalized, shipToWaypoint.magnitude);
                //for (int k = 0; k < hits.Length; k++)
                //{
                //    if (hits[k].collider)
                //    {
                //        if (hits[k].transform.tag == "Asteroid")
                //        {
                //            Debug.Log("AsteroidFailure");
                //            return TaskStatus.Failure;
                //        }
                //    }
                //}

                Vector2 diff = target.Value - Player.Value;
                float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                behaviourTree.SetVariableValue("Rotation", rot);
                //Debug.Log(Vector2.Distance(target.Value, Player.Value) + " " + hyperion.currentData.WayPoints[0].Radius);

            }
            return TaskStatus.Running;
        }

    }
}
