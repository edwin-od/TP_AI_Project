using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
using BehaviorDesigner.Runtime;

namespace Hyperion
{
    public class HyperionTeamController : BaseSpaceShipController
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
        public SpaceShipView mySpaceship;
        public bool isBlocked = false;
        public BehaviorTree behaviourTree;

        public override void Initialize(SpaceShipView spaceship, GameData data)
        {
            currentData = data;
            mySpaceship = spaceship;
        }

        public override InputData UpdateInput(SpaceShipView spaceship, GameData data)
        {
            currentData = data;
            mySpaceship = spaceship;
            SpaceShipView otherSpaceship = data.GetSpaceShipForOwner(1 - spaceship.Owner);

            behaviourTree.SetVariableValue("EnergyLevel", spaceship.Energy);
            behaviourTree.SetVariableValue("Score", (float)spaceship.Score);
            behaviourTree.SetVariableValue("EnemyPosition", otherSpaceship.Position);
            behaviourTree.SetVariableValue("PlayerPosition", spaceship.Position);

            Orientation = (SharedFloat)behaviourTree.GetVariable("Rotation");

            targetOrient = NormaliseValue(Orientation.Value);

            if (spaceship.Energy > spaceship.ShootEnergyCost)
            {
                float curEnerg = spaceship.Energy;
                RaycastHit2D[] hits = Physics2D.RaycastAll(spaceship.Position, new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * spaceship.Orientation), Mathf.Cos(Mathf.Deg2Rad * spaceship.Orientation)), Vector2.Distance(spaceship.Position, otherSpaceship.Position));
                bool canShoot = true;
                for(int k = 0; k < hits.Length; k++) 
                {
                    if (hits[k].collider) 
                    {
                        if (hits[k].transform.tag == "Asteroid" || hits[k].transform.tag == "Wall")
                        {
                            canShoot = false;
                            break;
                        }
                    }
                }

                if(canShoot) 
                {
                    needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.5f);
                    if (needShoot && data.Bullets.Count == 0)
                        curEnerg -= spaceship.ShootEnergyCost;
                    else
                        needShoot = false;
                }

                hits = Physics2D.RaycastAll(spaceship.Position, spaceship.Velocity.normalized);
                if (curEnerg > spaceship.ShockwaveEnergyCost)
                {
                    for (int k = 0; k < hits.Length; k++)
                    {
                        if (hits[k].collider)
                        {
                            if (hits[k].transform.tag == "Mine" && Vector2.Distance(spaceship.Position, hits[k].transform.position) < 2f)
                            {
                                for(int i=0; i< data.Mines.Count;i++)
                                {
                                    if(data.Mines[i].Position == (Vector2)hits[k].transform.position && data.Mines[i].IsActive)
                                        needShockwawe = true;
                                }
                            }
                        }
                    }
                }
            }
            if (otherSpaceship.HitCountdown > 0 && spaceship.Energy < 0.6f)
                thrust = 0f;
            else
                thrust = 1f;
            InputData input = new InputData(thrust, targetOrient, needShoot, needMine, needShockwawe);
            needMine = false;
            needShockwawe = false;
            needShoot = false;

            return input;
        }

        public float NormaliseValue(float value)
        {
            if (value < 0)
                value += 360;
            else if (value > 360)
                value -= 360;

            return value;
        }

    }

}