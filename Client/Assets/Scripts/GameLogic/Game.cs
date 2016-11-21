using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏相关
/// </summary>
public class Game
{
    private GameStateMgr m_gameStateMgr = new GameStateMgr();

    public void Start()
    {
        m_gameStateMgr.Start();

        InitGameState();
    }
    /// <summary>
    /// 初始化游戏状态
    /// </summary>
    private void InitGameState()
    {
        GameStateFactory.Build(m_gameStateMgr);
        m_gameStateMgr.SwitchTo(GameStateID.ST_BOOT, new GameStateParam());
    }

    public void Update()
    {

    }

    public void LateUpdate()
    {

    }
}
