using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Hyperion
{
    public class GoTo : Action
    {
        public BehaviorTree behaviourTree;
        private HyperionTeamController hyperion;
        public SharedVector2 target;
        public SharedVector2 Player;
        public float count = 0;
        public float abandonTime = 3;

        public override void OnStart()
        {
            hyperion = gameObject.GetComponent<HyperionTeamController>();
            Vector2 diff = target.Value - Player.Value;
            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            behaviourTree.SetVariableValue("Rotation", rot);
            count = 0;
        }
        public override TaskStatus OnUpdate()
        {
            count += Time.deltaTime;
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
                            return TaskStatus.Failure;
                        }
                    }
                }
            }
            if (hyperion && hyperion.currentData != null)
            {
                if (Vector2.Distance(target.Value, Player.Value) < hyperion.currentData.WayPoints[0].Radius)
                {
                    hyperion.thrust = 0.0f;
                    return TaskStatus.Success;
                }
                else
                {
                    hyperion.thrust = 1.0f;
                }

                Vector2 diff = target.Value - Player.Value;
                float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                behaviourTree.SetVariableValue("Rotation", rot);

            }
            return TaskStatus.Running;
        }

    }
}
