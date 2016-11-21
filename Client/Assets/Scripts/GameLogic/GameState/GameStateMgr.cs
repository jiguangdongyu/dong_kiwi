using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 游戏状态控制
/// </summary>
public class GameStateMgr
{
    private GameState m_activeState;    //当前激活的状态
    private int m_oldActiveStateID;       //上一次激活状态ID
    private Dictionary<int, GameState> m_stateDic = new Dictionary<int, GameState>();
    private Stack<GameStateParam> m_waitStateParamStack = new Stack<GameStateParam>();        //后进先出
    private Stack<int> m_waitStateStack = new Stack<int>();

    /// <summary>
    /// 清空等待激活状态集合
    /// </summary>
    public void ClearWairingState()
    {
        m_waitStateStack.Clear();
        m_waitStateParamStack.Clear();
    }

    /// <summary>
    /// 获取游戏状态
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameState GetState(int id)
    {
        if (!m_stateDic.ContainsKey(id))
            return null;
        return m_stateDic[id];
    }

    /// <summary>
    /// GUI
    /// </summary>
    public void OnGUI()
    {
        if (m_activeState != null)
            m_activeState.OnGUI();
    }

    /// <summary>
    /// 激活最后进入的游戏状态
    /// </summary>
    public void PopWaitingState()
    {
        if (m_waitStateStack.Count > 0)
        {
            int next = m_waitStateStack.Pop();
            GameStateParam param = m_waitStateParamStack.Pop();
            SwitchTo(next, param);
        }
    }

    /// <summary>
    /// 推入待机活游戏状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="param"></param>
    public void PushWaitingState(int id, GameStateParam param)
    {
        if (!m_stateDic.ContainsKey(id))
        {
            Logger.Log("invalid state :" + id);
        }
        else
        {
            m_waitStateStack.Push(id);
            m_waitStateParamStack.Push(param);
        }
    }


    /// <summary>
    /// 游戏状态注册
    /// </summary>
    /// <param name="st"></param>
    public void Register(GameState st)
    {
        if (st != null)
        {
            if (m_stateDic.ContainsKey(st.ID))
            {
                Logger.Log("state duplicate :" + st.ID);
            }
            else
            {
                st.SetMgr(this);
                m_stateDic[st.ID] = st;
            }
        }
    }

    /// <summary>
    /// 移出待激活游戏状态
    /// </summary>
    /// <param name="id"></param>
    public void RemoveWaitingState(int id)
    {
        if (m_waitStateStack.Contains(id))
        {
            m_waitStateStack.Pop();
            m_waitStateParamStack.Pop();
        }
    }

    /// <summary>
    /// 停止运行
    /// </summary>
    public void ShutDown()
    {
        if (m_activeState != null)
        {
            m_activeState.Exit();
            m_activeState = null;
        }

        m_waitStateStack.Clear();
        m_waitStateParamStack.Clear();
        foreach (GameState state in m_stateDic.Values)
        {
            state.Release();
        }
        m_stateDic.Clear();
    }

    public void Start()
    {

    }

    /// <summary>
    /// 进入下一游戏状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="param"></param>
    public void SwitchTo(int next, GameStateParam param)
    {
        if (!this.m_stateDic.ContainsKey(next))
        {
            Logger.Log("invalid next state :" + next);
        }
        else
        {
            if (m_activeState != null)
            {
                m_oldActiveStateID = m_activeState.ID;
                m_activeState.Exit();
            }
            m_activeState = m_stateDic[next];
            if (m_activeState != null)
            {
                m_activeState.Enter(param);
            }
        }
    }

    public void UnRegister(int id)
    {
        if (m_stateDic.ContainsKey(id))
        {
            m_stateDic[id].SetMgr(null);
            m_stateDic.Remove(id);
        }
    }

    public void Update()
    {
        if (m_activeState != null)
        {
            m_activeState.Update();
        }
    }

    public GameState ActiveState
    {
        get { return m_activeState; }
    }

    public int OldActiveStateID
    {
        get { return m_oldActiveStateID; }
    }
}
