//Jamo + Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class Slash0 : SlashBase
    {
        

        public Slash0( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Slash0";
            index = 0;
            hitBox = player.hitBoxes.slash0;
            
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            speedMod = 2f;
        }

        public override void OnStateFixedUpdate()
        {
            player.doMovement(speedMod);
            if(speedMod >= 0.05f)
            {
                speedMod /= 1.2f;
            }
        }











































        



    
    }
}
