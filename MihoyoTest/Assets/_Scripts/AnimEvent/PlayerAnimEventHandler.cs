using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Update Animation Event Type Here
/// </summary>
public enum EPlayerAnimNotify
{
    OnAttackOneStartInput,
    OnAttackOneEndInput,
    OnAttackTwoStartInput,
    OnAttackTwoEndInput,
    OnAttackThreeStartInput,
    OnAttackThreeEndInput,
    OnAttackOneEnd,
    OnAttackTwoEnd,
    OnAttackThreeEnd,
    OnJumpEndStartPreInput,
    OnJumpEndEnd,
    OnRunEndStartPreInput,
    OnRunEndEnd,
    OnHitEnd,
    OnBlockStartInput,
    OnBlockEndInput,
    OnBlockEnd,
    OnAttackOneEffect,
    OnAttackTwoEffect,
    OnAttackThreeEffect,
    OnKillStartEnd,
    OnKillGo,
    OnKillEnd,
    OnKillEffect
}

public class PlayerAnimEventHandler : AnimEventHandler<EPlayerAnimNotify>
{
}
