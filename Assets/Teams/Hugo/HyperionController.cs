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


		public BehaviorTree behaviourTree;

		public override void Initialize(SpaceShipView spaceship, GameData data)
		{
		}
		
		public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
		{
			SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);
			thrust = 1.0f;
			targetOrient = spaceship.Orientation + 90.0f;
			needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.15f);
			return new InputData(thrust, targetOrient, needShoot, needMine, needShockwawe);
			
		}
	}

}
