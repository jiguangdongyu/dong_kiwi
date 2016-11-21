using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// 项目启动入口
/// </summary>
public class App
{
    private static App m_app;
    private LevelMgr m_levelMgr = new LevelMgr();
    private ResourcesMgr m_resourcesMgr = new ResourcesMgr();
    private Game m_game = new Game();
    GameObject m_globalGameObject;

    public void Start()
    {
        m_levelMgr.Start();
        m_resourcesMgr.Start();

        m_game.Start();
    }

    public void Update()
    {
        m_levelMgr.Update();
        m_resourcesMgr.Update();

        m_game.Update();
    }

    public void LateUpdate()
    {
        m_game.LateUpdate();
    }

    public static void Create(GameObject obj = null)
    {
        m_app = new App();
        if (obj != null)
        {
            m_app.m_globalGameObject = obj;
        }
    }

    public static App Instance
    {
        get
        {
            return m_app;
        }
    }

    public static Game Game
    {
        get { return m_app.m_game; }
    }

    public static LevelMgr LevelMgr
    {
        get
        {
            return m_app.m_levelMgr;
        }
    }

    public static ResourcesMgr ResourcesMgr
    {
        get
        {
            return m_app.m_resourcesMgr;
        }
    }
}
