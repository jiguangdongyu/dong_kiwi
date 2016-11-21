using UnityEngine;
using System.Collections;

internal class SwitchingStateParam : GameStateParam
{
    public SwitchingStateParam()
    {
    }
}

/// <summary>
/// 过渡状态,一般用于Loading场景和其他场景之间的切换效果
/// </summary>
public class SwitchingState : GameState
{
    public SwitchingState(int id)
        : base(id)
    {
    }

    public override void Enter(GameStateParam param)
    {
        Logger.Log("enter switch state");


    }

    public override void Exit()
    {

    }

    public override void OnGUI()
    {

    }

    public override void Release()
    {

    }
}