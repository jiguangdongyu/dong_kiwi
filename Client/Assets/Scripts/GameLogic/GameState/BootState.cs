using UnityEngine;
using System.Collections;

internal class BootStateParam : GameStateParam
{
}
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
        GamePrepare();
    }

    public override void Exit()
    {

    }

    /// <summary>
    /// 启动第一场景后加载游戏所需的数据
    /// </summary>
    private void GamePrepare()
    {
        App.StringResMgr.InitStringConf();
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