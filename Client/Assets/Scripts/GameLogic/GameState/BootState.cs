using UnityEngine;
using System.Collections;

/// <summary>
/// 开场游戏状态
/// </summary>
public class BootState : GameState
{
    public BootState(int id)
        : base(id)
    {
    }

    public override void Enter(GameStateParam param)
    {
        Logger.Log("enter boot state");
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

    public override void Update()
    {

    }
}