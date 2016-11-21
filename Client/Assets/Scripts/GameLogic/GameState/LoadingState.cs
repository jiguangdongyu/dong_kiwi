using UnityEngine;
using System.Collections;

internal class LoadingStateParam : GameStateParam
{
    public string sceneName;
    public bool autoCloseUI;

    public LoadingStateParam(string scene, bool autoClose = false)
    {
        sceneName = scene;
        autoCloseUI = autoClose;
    }
}

/// <summary>
/// Loading场景
/// </summary>
public class LoadingState : GameState
{
    private bool m_authClose;
    private LevelListener m_levelListener;
    private bool m_bLoadingLaunched;
    private LoadingStateParam m_loadingStateParam;
    private float m_timer;
    private float m_totalTime;
    

    public LoadingState(int id)
        : base(id)
    {
    }

    public override void Enter(GameStateParam param)
    {
        Logger.Log("enter loading state");


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

    /// <summary>
    /// 运行游戏场景 TODO by ljdong
    /// </summary>
    private void LaunchLevel()
    {
        m_authClose = m_loadingStateParam.autoCloseUI;
        if (m_loadingStateParam.sceneName != null)
        {
            m_levelListener = new LevelListener();
            m_levelListener.callBack = new LevelListener.OnLevelLoaded(this.OnLevelLoaded);

            //if(App.LevelMgr.)
        }
    }

    private void OnLevelLoaded(string nameScene)
    {
        base.Mgr.PopWaitingState();
    }

    public override void Update()
    {
        if ((m_timer > m_totalTime) && !m_bLoadingLaunched)
        {
            LaunchLevel();
            m_bLoadingLaunched = true;
        }
        m_timer += Time.deltaTime;
    }
}