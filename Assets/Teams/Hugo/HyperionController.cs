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
		public float Orientation = 0f;


		public BehaviorTree behaviourTree;

		public override void Initialize(SpaceShipView spaceship, GameData data)
		{
		}
		
		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);

			behaviourTree.SetVariableValue("EnergyLevel", spaceship.Energy);
			behaviourTree.SetVariableValue("Score",  (float)spaceship.Score);
			behaviourTree.SetVariableValue("EnemyPosition",  otherSpaceship.Position);
			behaviourTree.SetVariableValue("ClosestPoint",data.WayPoints[0].Position);
			
			thrust = 1.0f;
			
			targetOrient = Orientation;
			needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			InputData input =new InputData(thrust, targetOrient, needShoot, needMine, needShockwawe);
			needMine = false;
			needShockwawe = false;
			needShoot = false;
			
			return input;

			
		}
	}

}
