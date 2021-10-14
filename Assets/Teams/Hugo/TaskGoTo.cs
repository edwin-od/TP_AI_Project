using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Hyperion;
using DoNotModify;

public class TaskGoTo : Action
{
    public BehaviorTree behaviourTree;
    private HyperionController hyperion;
    public SharedVector2 target;
    public SharedVector2 Player;
   // public float SlowDownRadiusMultiplier;

    public override void OnStart()
    {
        hyperion = gameObject.GetComponent<HyperionController>();
        Vector2 diff = target.Value - Player.Value;
        float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        behaviourTree.SetVariableValue("Rotation", rot);
    }
    public override TaskStatus OnUpdate()
    {
        for(int i=0; i < hyperion.currentData.WayPoints.Count; i++) {
            if(hyperion.currentData.WayPoints[i].Position == target.Value) {
                if (hyperion.mySpaceship.Owner == hyperion.currentData.WayPoints[i].Owner) {
                    return TaskStatus.Failure;
                }
            }
        }
        if (hyperion && hyperion.currentData != null) {
            if (Vector2.Distance(target.Value, Player.Value) < hyperion.currentData.WayPoints[0].Radius) {
                hyperion.thrust = 0.0f;
                return TaskStatus.Success;
            }
            //else if(Vector2.Distance(target.Value, Player.Value) < SlowDownRadiusMultiplier * hyperion.currentData.WayPoints[0].Radius) {
            //    hyperion.thrust = 0.1f;
            //} 
            else {
                hyperion.thrust = 1.0f;
            }

            Vector2 shipToWaypoint = new Vector2(target.Value.x - Player.Value.x, target.Value.y - Player.Value.y);
            Debug.DrawRay(hyperion.mySpaceship.Position, shipToWaypoint, Color.red);
            RaycastHit2D[] hits = Physics2D.RaycastAll(Player.Value, shipToWaypoint.normalized, shipToWaypoint.magnitude);
            for (int k = 0; k < hits.Length; k++) {
                if (hits[k].collider) {
                    if (hits[k].transform.tag == "Asteroid")
                        return TaskStatus.Failure;
                }
            }

            Vector2 diff = target.Value - Player.Value;
            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            behaviourTree.SetVariableValue("Rotation", rot);
            //Debug.Log(Vector2.Distance(target.Value, Player.Value) + " " + hyperion.currentData.WayPoints[0].Radius);
            
        }
        return TaskStatus.Running;
    }

}
