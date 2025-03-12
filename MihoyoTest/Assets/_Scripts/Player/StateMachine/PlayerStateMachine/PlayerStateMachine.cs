using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerController Player { get; }
    public StateReusableData ReusableData { get; }
    
    public PlayerIdleState IdleState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerRunEndState RunEndState { get; }
    
    public PlayerJumpStartState JumpStartState { get; }
    public PlayerJumpLoopState JumpLoopState { get; }
    public PlayerJumpEndState JumpEndState { get; }
    
    public PlayerAttackOneState AttackOneState { get; }
    public PlayerAttackTwoState AttackTwoState { get; }
    public PlayerAttackHeavyState AttackHeavyState { get; }

    public PlayerStateMachine(PlayerController player)
    {
        Player = player;
        ReusableData = new StateReusableData(player.PlayerData);
        
        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        RunEndState = new PlayerRunEndState(this);
        
        JumpStartState = new PlayerJumpStartState(this);
        JumpLoopState = new PlayerJumpLoopState(this);
        JumpEndState = new PlayerJumpEndState(this);
        
        AttackOneState = new PlayerAttackOneState(this);
        AttackTwoState = new PlayerAttackTwoState(this);
        AttackHeavyState = new PlayerAttackHeavyState(this);
    }
}
