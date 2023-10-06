using UnityEngine;
using Gabevlogd.Patterns;
using System.Collections.Generic;

public class PlayerStateMachine : StatesManager<Enumerators.PlayerState>
{
    public PlayerData PlayerData;

    public PlayerStateMachine(ref PlayerData playerData) 
    {
        PlayerData = playerData;
        InitStatesManager();
    }

    protected override void InitStates()
    {
        AllStates.Add(Enumerators.PlayerState.Idle, new Idle(Enumerators.PlayerState.Idle, this));
        AllStates.Add(Enumerators.PlayerState.MoveLeft, new Left(Enumerators.PlayerState.MoveLeft, this));
        AllStates.Add(Enumerators.PlayerState.MoveRight, new Right(Enumerators.PlayerState.MoveRight, this));
        AllStates.Add(Enumerators.PlayerState.Jump, new Jump(Enumerators.PlayerState.Jump, this));
        AllStates.Add(Enumerators.PlayerState.DoubleJump, new DoubleJump(Enumerators.PlayerState.DoubleJump, this));

        CurrentState = AllStates[Enumerators.PlayerState.Idle];
        CurrentState.OnEnter();
    }
}
