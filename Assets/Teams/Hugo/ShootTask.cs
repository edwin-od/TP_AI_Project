using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DoNotModify;
using BehaviorDesigner.Runtime.Tasks;
using Hyperion;

public class ShootTask : Action
{
    private HyperionController hyperion;
    public BehaviorTree behaviourTree;
    public SharedVector2 Player;
    bool canShoot = false;
    SpaceShipView otherSpaceship;

    public override void OnStart()
    {
        canShoot = false;
        hyperion = gameObject.GetComponent<HyperionController>();
        otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.mySpaceship.Owner);
        Vector2 shipToWaypoint = new Vector2(otherSpaceship.Position.x - Player.Value.x, otherSpaceship.Position.y - Player.Value.y);
        Debug.DrawRay(Player.Value, shipToWaypoint, Color.yellow);
        RaycastHit2D[] hits = Physics2D.RaycastAll(Player.Value, shipToWaypoint.normalized, shipToWaypoint.magnitude);
        for (int k = 0; k < hits.Length; k++)
        {
            if (hits[k].collider)
            {
                float angle = Mathf.Atan2(shipToWaypoint.y, shipToWaypoint.x) * Mathf.Rad2Deg;

                if (hits[k].transform.tag == "Player" && (Vector2)hits[k].transform.position != hyperion.mySpaceship.Position && hits[k].transform.tag != "Asteroid")
                {
                    Debug.Log("can shoot " + hits[k].transform.gameObject.name);
                    //hyperion.needShoot = AimingHelpers.CanHit(hyperion.mySpaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
                    canShoot = true;

                }

            }
        }

    }
    public override TaskStatus OnUpdate()
    {
        Vector2 shipToWaypoint = new Vector2(otherSpaceship.Position.x - Player.Value.x, otherSpaceship.Position.y - Player.Value.y);
        Debug.DrawRay(Player.Value, shipToWaypoint, Color.yellow);
        if (canShoot)
        {
            behaviourTree.SetVariableValue("TargetPos", otherSpaceship.Position);
            hyperion.needShoot = AimingHelpers.CanHit(hyperion.mySpaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
        }
        //return TaskStatus.Running;
        if(hyperion.needShoot) return TaskStatus.Success;
        else{ return TaskStatus.Failure; }
        //if(hyperion.needShoot) hyperion.needShoot = false;
    }
}
