
using DG.Tweening;
using UnityEngine;

public class PlayerKillState : PlayerReflectionState
{
    public PlayerKillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        StartAnimation(PlayerStateMachine.Player.AnimationData.KillParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        CameraManager.Instance.DeactivateStateCamera();
        StopAnimation(PlayerStateMachine.Player.AnimationData.KillParameter);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        var direction = PlayerStateMachine.ReusableData.TargetEnemy.transform.position - PlayerStateMachine.Player.transform.position;
        Rotate(direction, false);
    }

    public void Warp()
    {
        var enemy = PlayerStateMachine.ReusableData.TargetEnemy;
        var targetPosition = enemy.EnemyAIKnowledge.killTransform.position;
        PlayerStateMachine.Player.SkinMeshHandler.ShowSkin(false);
        PlayerStateMachine.Player.transform.root.transform.DOMove(targetPosition, 0.2f).OnComplete(() =>
        {
            CameraManager.Instance.ShakeCamera(1f);
            PlayerStateMachine.Player.SkinMeshHandler.ShowSkin(true);
            enemy.GetKilled();
        });
    }
}