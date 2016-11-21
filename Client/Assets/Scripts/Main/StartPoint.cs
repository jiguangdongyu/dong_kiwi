using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// 项目启动入口
/// </summary>
public class StartPoint : MonoBehaviour
{
    private float m_delayToStartGame = 0.2f;
    private float m_timeSinceStart;

    static int ref_ID;
    int my_ref_ID;

    void Awake()
    {
        ref_ID++;
        my_ref_ID = ref_ID;

        if (my_ref_ID > 1)
        {
            Destroy(gameObject);
            return;
        }

        m_timeSinceStart = 0;

        Application.backgroundLoadingPriority = ThreadPriority.Low;

        // dim screen after 3 minutes
        Screen.sleepTimeout = 180;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_delayToStartGame);
        StartGame();
    }

    void StartGame()
    {
        try
        {
            if (App.Instance == null)
            {
                App.Create(base.gameObject);
                App.Instance.Start();

                UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            }
        }
        catch (System.Exception ex)
        {
            Logger.LogError("App Start Exception: " + ex.Message + " " + ex.StackTrace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (my_ref_ID > 1)
            return;

        m_timeSinceStart += Time.deltaTime;
        if (m_timeSinceStart < m_delayToStartGame)
            return;

        try
        {
            if (App.Instance != null)
            {
                App.Instance.Update();
            }
        }
        catch (System.Exception ex)
        {
            Logger.LogError("App Update Exception: " + ex.Message + " " + ex.StackTrace);
        }
    }

    void LateUpdate()
    {
        try
        {
            if (App.Instance != null)
                App.Instance.LateUpdate();
        }
        catch (System.Exception ex)
        {
            Logger.LogError("App LateUpdate Exception: " + ex.Message + " " + ex.StackTrace);
        }
    }
}
