using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 场景管理器
/// </summary>
public class LevelMgr
{
    private LevelListener m_levelListener;
    private bool m_loadingScene;                  //过渡场景
    private string m_loadingSceneName;            //过渡场景名
    private readonly List<string> m_sceneBundleList;    //

    private AsyncOperation m_async;
    private float m_loadingTime = 0;
    private bool m_bAlmostLoaded = false;
    private float m_additionalLoadingTime;

    private static void ClearOldSceneResources()
    {

    }

    public void LoadLevel(string sceneName, LevelListener listener)
    {
        Logger.LogYellow("load level:" + sceneName);
        if (m_loadingScene)
        {
            Logger.LogWarning("other scene is loading...");
        }
        else if (string.IsNullOrEmpty(sceneName))
        {
            Logger.LogWarning("scene name is null");
        }
        else if (listener == null)
        {
            Logger.LogWarning("listener is null: " + listener);
        }
        else
        {
            m_loadingSceneName = sceneName;
            m_levelListener = listener;
            SetStartLoading();
            Application.LoadLevel(sceneName);
        }
    }

    public void LoadLevelAsync(string sceneName, LevelListener listener)
    {
        m_async = null;
        Logger.LogYellow("load level async " + sceneName + " :" + Time.realtimeSinceStartup);
        if (m_loadingScene)
        {
            Logger.LogWarning("other scene is loading...");
        }
        else if(string.IsNullOrEmpty(sceneName))
        {
            Logger.LogWarning("scene name is null");
        }
        else if (listener == null)
        {
            Logger.LogWarning("linster is null: " + listener);
        }
        else
        {
            m_loadingSceneName = sceneName;
            m_levelListener = listener;
            SetStartLoading();
            m_async = Application.LoadLevelAsync(sceneName);
            m_async.allowSceneActivation = false;
            m_bAlmostLoaded = false;
        }
    }

    private void SetStartLoading()
    {
        m_loadingScene = true;
        m_loadingTime = 0;
    }

    public void Update()
    {
        m_loadingTime += Time.deltaTime;

        if (m_loadingScene && m_loadingTime > 0.1f)             //预留0.1s再开始加载
        {
            if (this.m_async != null)
            {
                if (!m_bAlmostLoaded && m_async.progress >= 0.9f)        //进度最多到0.9f
                {
                    m_async.allowSceneActivation = true;                //进入异步加载的场景
                    m_bAlmostLoaded = true;
                    m_additionalLoadingTime = 0;
                    return;
                }
                else
                {
                    if (m_bAlmostLoaded)
                    {
                        m_additionalLoadingTime += Time.deltaTime;
                        if (m_additionalLoadingTime < 0.1f)           //进入场景后预留0.1f做相应处理
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            m_loadingScene = false;
            ClearOldSceneResources();
            m_levelListener.callBack(m_loadingSceneName);
            m_levelListener = null;
            m_async = null;
        }
    }
}
