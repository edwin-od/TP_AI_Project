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
    public SharedVector2 Player;
    bool canShoot;
    SpaceShipView otherSpaceship;

    public override void OnStart()
    {
        hyperion = gameObject.GetComponent<HyperionController>();
        otherSpaceship = hyperion.currentData.GetSpaceShipForOwner(1 - hyperion.SelfShip.Owner);
        Vector2 shipToWaypoint = new Vector2(otherSpaceship.Position.x - Player.Value.x, otherSpaceship.Position.y - Player.Value.y);
        Debug.DrawRay(Player.Value, shipToWaypoint, Color.yellow);
        RaycastHit2D[] hits = Physics2D.RaycastAll(Player.Value, shipToWaypoint.normalized, shipToWaypoint.magnitude);
        for (int k = 0; k < hits.Length; k++)
        {
            if (hits[k].collider)
            {
                
                if(hits[k].transform.tag == "Player" && (Vector2)hits[k].transform.position != hyperion.SelfShip.Position && hits[k].transform.tag != "Asteroid")
                {
                    Debug.Log("can shoot " + hits[k].transform.gameObject.name);
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
            hyperion.needShoot = AimingHelpers.CanHit(hyperion.SelfShip, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
            canShoot = false;
        }
        //if(hyperion.needShoot) hyperion.needShoot = false;
        return TaskStatus.Success;
    }
}
