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
    GameObject m_globalGameObject;

    public void Start()
    {
        m_levelMgr.Start();
        m_resourcesMgr.Start();
    }

    public void Update()
    {
        m_levelMgr.Update();
        m_resourcesMgr.Update();
    }

    public void LateUpdate()
    {

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

    public static LevelMgr LevelManager
    {
        get
        {
            return m_app.m_levelMgr;
        }
    }

    public static ResourcesMgr ResourcesManager
    {
        get
        {
            return m_app.m_resourcesMgr;
        }
    }
}
