using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerController player;

    public PlayerState.InputState InputState;
        public PlayerState.ComboAttackState ComboAttackState;
            public PlayerState.Slash0 Slash0;
            public PlayerState.Slash1 Slash1;
            public PlayerState.Slash2 Slash2;
            public PlayerState.Slash3 Slash3;
            public PlayerState.Slash4 Slash4;

        public PlayerState.HeavyCharge HeavyCharge;
        public PlayerState.HeavySlash HeavySlash;

        public PlayerState.MovementState MovementState;
            public PlayerState.IdleMove IdleMove;
            public PlayerState.Dash Dash;

    public PlayerState.Hurt Hurt;

    public PlayerState.Standby Standby;

    public PlayerState.State currentState { get; private set; }
    public PlayerState.State transitionExitState { get; set; }
    public PlayerState.State previousState { get; set; }

    private void Start()
    {
        player = GetComponent<PlayerController>();

        InputState = new PlayerState.InputState( this );
            ComboAttackState = new PlayerState.ComboAttackState( this );
                Slash0 = new PlayerState.Slash0( this );
                Slash1 = new PlayerState.Slash1( this );
                Slash2 = new PlayerState.Slash2( this );
                Slash3 = new PlayerState.Slash3( this );
                Slash4 = new PlayerState.Slash4( this );
                
            HeavyCharge = new PlayerState.HeavyCharge( this );
            HeavySlash = new PlayerState.HeavySlash( this );

            MovementState = new PlayerState.MovementState( this );
                IdleMove = new PlayerState.IdleMove( this );
                Dash = new PlayerState.Dash( this );

        Hurt = new PlayerState.Hurt( this );
        Standby = new PlayerState.Standby( this );

        SetState(IdleMove);//change to default state, idle
    }

    public void SetState( PlayerState.State state )
    {        
        currentState = state;
        currentState.OnStateEnter();
    }

    private void Update()
    {
        currentState.OnParentStateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.OnParentStateFixedUpdate();
    }
}
