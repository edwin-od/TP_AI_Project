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
            behaviourTree.SetVariableValue("EnemyPosition",  otherSpaceship.Position);
            behaviourTree.SetVariableValue("PlayerPosition", spaceship.Position);

            Orientation = (SharedFloat)behaviourTree.GetVariable("Rotation");

            targetOrient = NormaliseValue(Orientation.Value);

            if (spaceship.Energy>0.51f)
            {
                needShoot = AimingHelpers.CanHit(spaceship, otherSpaceship.Position, otherSpaceship.Velocity, 0.5f);
            }
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
//fonction commentary()
//{
//    float highBound = NormaliseValue(targetOrient + detectionRange);
//    float lowBound = NormaliseValue(targetOrient - detectionRange);
//    float velo = NormaliseValue(Mathf.Atan2(spaceship.Velocity.x, spaceship.Velocity.y) * Mathf.Rad2Deg);
//    float veryHighBound = NormaliseValue(velo + 90);
//    float veryLowBound = NormaliseValue(velo - 90);


//    float slowVelocity = 0.2f;

//    if (!needToHairpin && spaceship.Velocity.magnitude > slowVelocity && spaceship.Orientation > veryHighBound && spaceship.Orientation < veryLowBound)
//    {
//        Debug.Log("start demi tour");

//        Vector2 diff = -spaceship.Velocity;
//        float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
//        targetOrient = NormaliseValue(rot);

//        needToHairpin = true;
//    }

//    if (!needToHairpin)
//    {
//        Debug.Log("no demi tour : " + spaceship.Velocity.magnitude);

//        if (spaceship.Orientation < highBound && spaceship.Orientation > lowBound)
//        {
//            thrust = 1f;
//        }
//        else
//        {
//            thrust = 0f;

//        }

//    }
//    else
//    {
//        if (spaceship.Velocity.magnitude > slowVelocity)
//        {
//            Vector2 diff = -spaceship.Velocity;
//            float rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
//            float vel = NormaliseValue(rot);

//            float velLowBound = NormaliseValue(vel - 10);
//            float velHighBound = NormaliseValue(vel + 10);

//            if (spaceship.Orientation < velHighBound && spaceship.Orientation > velLowBound)
//            {
//                Debug.Log("demi tour frein : acceleration+");
//                thrust = 1f;
//            }
//            else
//            {
//                Debug.Log("demi tour frein : nope");

//                thrust = 0f;
//            }
//            Debug.Log(velLowBound + "     " + vel + "     " + velHighBound);

//        }
//        else
//        {
//            Debug.Log("demi tour fini");
//            thrust = 0f;
//            needToHairpin = false;
//        }
//    }
//}
