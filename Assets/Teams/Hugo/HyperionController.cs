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
		public bool PointSet =  false;
		

		public BehaviorTree behaviourTree;

		public override void Initialize(SpaceShipView spaceship, GameData data)
		{

		}
		
		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
            if (!PointSet)
            {
				GetClosestPoint(spaceship, data);
            }
			behaviourTree.SetVariableValue("EnergyLevel", spaceship.Energy);
			behaviourTree.SetVariableValue("Score",  (float)spaceship.Score);
			behaviourTree.SetVariableValue("EnemyPosition",  otherSpaceship.Position);
			behaviourTree.SetVariableValue("PlayerPosition",  spaceship.Position);
			Orientation = (SharedFloat)behaviourTree.GetVariable("Orientation");
			
            
            
			targetOrient = Orientation.Value ;
            
			thrust = 1.0f;
			
			
			needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			InputData input =new InputData(thrust, targetOrient, needShoot, needMine, needShockwawe);
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
	}



}
