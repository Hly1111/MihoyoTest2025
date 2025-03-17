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
    
    public PlayerHitState HitState { get; }
    
    public PlayerBlockState BlockState { get; }
    public PlayerWaitForKillState WaitForKillState { get; }
    public PlayerKillState KillState { get; }
    
    public PlayerKillEndState KillEndState { get; }
    
    public PlayerKillEndPoseState KillEndPoseState { get; }

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
        
        HitState = new PlayerHitState(this);
        
        BlockState = new PlayerBlockState(this);
        WaitForKillState = new PlayerWaitForKillState(this);
        KillState = new PlayerKillState(this);
        KillEndState = new PlayerKillEndState(this);
        KillEndPoseState = new PlayerKillEndPoseState(this);
        
        BindAllEvents();
    }

    private void BindAllEvents()
    {
        Player.PlayerAnimEventHandler.BindAllAnimEvents(this);
    }
    
    private void UnbindAllEvents()
    {
        Player.PlayerAnimEventHandler.UnbindAllAnimEvents(this);
    }
    
    #region AttackOneState
    private void AnimEvent_OnAttackOneStartInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed += AttackOneState.AttackDoneCallback;
    }

    private void AnimEvent_OnAttackOneEndInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed -= AttackOneState.AttackDoneCallback;
        if (ReusableData.AttackIndex == 2)
        {
            ChangeState(AttackTwoState);
        }
    }
    
    private void AnimEvent_OnAttackOneEnd()
    {
        if (ReusableData.AttackIndex == 1)
        {
            ChangeState(IdleState);
        }
    }

    private void AnimEvent_OnAttackOneEffect()
    {
        Player.VfxDataHandler.PlayVfx(EVfxType.AttackOne);
    }
    #endregion
    
    #region AttackTwoState
    
    private void AnimEvent_OnAttackTwoStartInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed += AttackTwoState.AttackDoneCallback;
    }
    
    private void AnimEvent_OnAttackTwoEndInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed -= AttackTwoState.AttackDoneCallback;
        if (ReusableData.AttackIndex == 3)
        {
            ChangeState(AttackHeavyState);
        }
    }
    
    private void AnimEvent_OnAttackTwoEnd()
    {
        if (ReusableData.AttackIndex == 2)
        {
            ChangeState(IdleState);
        }
    }
    
    private void AnimEvent_OnAttackTwoEffect()
    {
        Player.VfxDataHandler.PlayVfx(EVfxType.AttackTwo);
    }
    
    #endregion
    
    #region AttackHeavyState
    
    private void AnimEvent_OnAttackThreeStartInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed += AttackHeavyState.AttackDoneCallback;
    }
    
    private void AnimEvent_OnAttackThreeEndInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed -= AttackHeavyState.AttackDoneCallback;
        if (ReusableData.AttackIndex == 1)
        {
            ChangeState(AttackOneState);
        }
        AttackHeavyState.AddPreInputCallback();
    }
    
    private void AnimEvent_OnAttackThreeEnd()
    {
        if (ReusableData.AttackIndex == 3)
        {
            ChangeState(IdleState);
        }
    }
    
    private void AnimEvent_OnAttackThreeEffect()
    {
        Player.VfxDataHandler.PlayVfx(EVfxType.AttackThree);
    }
    
    #endregion
    
    #region RunEndState
    
    private void AnimEvent_OnRunEndStartPreInput()
    {
        RunEndState.AddPreInputCallback();
    }
    
    private void AnimEvent_OnRunEndEnd()
    {
        RunEndState.RemovePreInputCallback();
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region HitState
    
    private void AnimEvent_OnHitEnd()
    {
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region JumpEndState
    
    private void AnimEvent_OnJumpEndStartPreInput()
    {
        JumpEndState.AddPreInputCallback();
    }
    
    private void AnimEvent_OnJumpEndEnd()
    {
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region BlockState
    
    private void AnimEvent_OnBlockStartInput()
    {
        BlockState.CanBlock = true;
    }
    
    private void AnimEvent_OnBlockEndInput()
    {
        BlockState.CanBlock = false;
    }
    
    private void AnimEvent_OnBlockEnd()
    {
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region WaitForKillState
    private void AnimEvent_OnKillStartEnd()
    {
        ChangeState(KillState);
    }
    #endregion
    
    #region KillState
    
    private void AnimEvent_OnKillGo()
    {
        KillState.Warp();
    }
    
    #endregion
    
    #region KillEndState
    private void AnimEvent_OnKillEffect()
    {
        Player.VfxDataHandler.PlayVfx(EVfxType.Kill);
    }
    private void AnimEvent_OnKillEnd()
    {
        ChangeState(KillEndPoseState);
    }
    #endregion
    
    #region KillEndPoseState
    
    private void AnimEvent_OnKillEndPoseEnd()
    {
        ChangeState(IdleState);
    }
    #endregion
}
