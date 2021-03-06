//Jamo + Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace PlayerState
{
    [Serializable]
    public class SlashBase : State
    {
        protected int index;
        protected State nextState;
        public GameObject hitBox;
        protected float speedMod = 1f;

        private Vector3 startRotation;
        private Vector3 startMovement;
        


        
        private Vector3 startPos;

        public SlashBase( PlayerStateMachine _fsm ) : base( _fsm )
        {
            name = "Slash Base";
            parent = fsm.ComboAttackState;
            
        }



        private State GetNextState()
        {
            switch(index)
            {
                case 0:
                    return fsm.Slash1;
                case 1:
                    return fsm.Slash2;
                case 2:
                    return fsm.Slash3;
                case 3:
                    return fsm.Slash4;
                default:
                    return null;
            }
        }



        public override void OnStateEnter()
        {
            player.inputs.basicAttack = false;  
            nextState = GetNextState();
            
            player.Slash();
            animator.Attack( index );

            // hitBox.SetActive(false);
            // hitBox.SetActive(true); //Disable these once hitbox anim events work

            speedMod = 1f;
        }



        public override void OnStateUpdate()
        {
            if( !animator.isAttack &&
                nextState != null &&
                player.inputs.basicAttack )
            {
                SetState( nextState );
                return;
            }
        }

        

        public override void OnStateFixedUpdate()
        {
            
            player.doMovement(speedMod);
            if(speedMod >= 0.05f)
            {
                speedMod /= 1.4f;
            }
        
        }



        public override void OnStateExit()
        {
            player.inputs.basicAttack = false;
            hitBox.SetActive(false);
        }
    }
}
