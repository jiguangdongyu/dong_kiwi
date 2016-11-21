using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏场景
/// </summary>
public class GameState
{
    private int m_id;
    private GameStateMgr m_gameStateMgr;

    public GameState(int id)
    {
        this.m_id = id;
    }

    public virtual void Enter(GameStateParam param)
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void OnGUI()
    {

    }

    public virtual void Release()
    {

    }

    public void SetMgr(GameStateMgr gsm)
    {
        this.m_gameStateMgr = gsm;
    }

    public virtual void Update()
    {

    }

    public int ID
    {
        get { return this.m_id; }
    }

    public GameStateMgr Mgr
    {
        get { return this.m_gameStateMgr; }
    }
}

/// <summary>
/// 场景所需参数
/// </summary>
public class GameStateParam
{
}
