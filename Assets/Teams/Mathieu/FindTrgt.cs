using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using Hyperion;

public class FindTrgt : Action
{
    public BehaviorTree behaviourTree;
    private HyperionController hyperion;
    public SharedVector2 target;
    public SharedVector2 Player;
    public SharedFloat Orientation;

    public override void OnStart()
    {
        //Vector2 closestPos = Vector2.zero;
        //for (int i = 0; i < data.WayPoints.Count; i++)
        //{
        //    if ((data.WayPoints[i].Owner == otherSpaceship.Owner || data.WayPoints[i].Owner == -1))
        //    {
        //        bool ignore = false;
        //        Vector2 shipToWaypoint = new Vector2(data.WayPoints[i].Position.x - spaceship.Position.x, data.WayPoints[i].Position.y - spaceship.Position.y);
        //        Debug.DrawRay(spaceship.Position, shipToWaypoint, Color.red);
        //        RaycastHit2D[] hits = Physics2D.RaycastAll(spaceship.Position, shipToWaypoint.normalized, shipToWaypoint.magnitude);
        //        for (int k = 0; k < hits.Length; k++)
        //        {
        //            if (hits[k].collider)
        //            {
        //                Debug.Log(hits[k].transform.gameObject.name);
        //                if (hits[k].transform.tag == "Asteroid")
        //                    ignore = true;
        //            }
        //        }

        //        if (!ignore)
        //        {
        //            if (closestPos == Vector2.zero)
        //            {
        //                closestPos = data.WayPoints[i].Position;
        //                continue;
        //            }

        //            float angle = Vector2.SignedAngle(spaceship.Position, data.WayPoints[i].Position);
        //            angle = NormaliseValue(angle);

        //            if (Vector2.Distance(spaceship.Position, closestPos) > Vector2.Distance(spaceship.Position, data.WayPoints[i].Position) && (angle < 10 || angle > 350))
        //            {
        //                closestPos = data.WayPoints[i].Position;
        //            }
        //        }
        //    }
        //}

        //behaviourTree.SetVariableValue("TargetPos", closestPos);
    }
    

}
