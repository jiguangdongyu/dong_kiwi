using UnityEngine;
using System.Collections;

internal class SwitchingStateParam : GameStateParam
{
    public SwitchingStyle Style;
    private readonly string[] SwitchingPrefabs = new string[] { "SceneSwitching/GateSwitching/SceneSwitchingMask", "SceneSwitching/CloudSwitching/SceneSwitchingCloudMask", "SceneSwitching/NormalSwitching/SwitchinRealisticPrefab", "SceneSwitching/FanSwitching/FanSwitchingPrefab" };
    public int TargetSceneGameStateID;
    public string TargetSceneName;
    public GameStateParam TargetSceneParam;
    public bool bManualClose;
    public bool bAdditive;

    public string PrefabPath
    {
        get
        {
            return this.SwitchingPrefabs[(int)this.Style];
        }
    }

    public enum SwitchingStyle
    {
        Gate,
        Cloud,
        Normal,
        Fan
    }
}

/// <summary>
/// 过渡状态,一般用于Loading场景和其他场景之间的切换效果
/// </summary>
public class SwitchingState : GameState
{
    private bool m_doneFadeIn;
    private bool m_doneLoaded;
    private LevelListener m_listener;
    private Animation m_maskAnimation;
    private GameObject m_maskObj;
    private bool m_startedFadeIn;
    private bool m_startedFadeOut;
    private float m_startLoadingTime;

    private float m_loadingSpeedIdle = 10;
    private float m_loadingP = 0;
    private float m_totalLoadingP = 100;
    private float m_levelLoadingThreshold = 70;

    private SwitchingStateParam m_stateParam;
    private const string m_switchingClipName = "Switching";
    private const string m_switchingInClipName = "SwitchingIn";
    private const string m_switchingOutClipName = "SwitchingOut";

    public bool m_bManualClose;
    private bool m_bManualCloseDone;
    public SwitchingState(int id)
        : base(id)
    {
#if UNITY_EDITOR
        m_loadingSpeedIdle = 35;
#endif
    }

    public override void Enter(GameStateParam param)
    {
        Logger.Log("enter switch state");

        m_loadingP = 0;
        m_stateParam = param as SwitchingStateParam;
        m_doneLoaded = false;
        m_startedFadeIn = false;
        m_doneFadeIn = false;
        m_startedFadeOut = false;

        this.m_bManualClose = this.m_stateParam.bManualClose;
        this.m_bManualCloseDone = false;

        ShowDarkMask(true);

        if (m_maskObj != null)
            UnityEngine.Object.Destroy(m_maskObj);

        Object orgin = Resources.Load(this.m_stateParam.PrefabPath);
        if (orgin != null)
        {
            this.m_maskObj = (GameObject)UnityEngine.Object.Instantiate(orgin);
            this.m_maskAnimation = this.m_maskObj.transform.GetComponentsInChildren<Animation>()[0];
            UnityEngine.Object.DontDestroyOnLoad(this.m_maskObj);
        }
    }

    public override void Exit()
    {
        this.m_maskObj.AddComponent<CustomUpdater>().UpdateProc = delegate(GameObject gameObject)
        {
            if (!m_bManualClose || (m_bManualClose && m_bManualCloseDone))
            {
                Logger.LogYellow("switching state exit...");
                UnityEngine.Object.Destroy(gameObject);
            }
        };
    }

    public void ManulCloseDone()
    {
        ShowDarkMask(false);
        m_bManualCloseDone = true;

        this.m_loadingP = this.m_totalLoadingP;

        Logger.LogYellow("close switching scene." + Time.realtimeSinceStartup);
    }

    public override void OnGUI()
    {

    }

    public override void Release()
    {

    }

    public override void Update()
    {
        this.m_loadingP += Time.deltaTime * m_loadingSpeedIdle;

        if (!m_startedFadeOut && !m_maskAnimation.IsPlaying(m_switchingInClipName) && !m_maskAnimation.IsPlaying(m_switchingOutClipName))
        {
            if (!m_startedFadeIn)
            {
                m_startedFadeIn = true;
                m_maskAnimation.Play(m_switchingInClipName);

                m_loadingP += 10;
            }
            else if (!m_doneFadeIn)
            {
                m_doneFadeIn = true;
                if (m_stateParam.TargetSceneName != null)
                {
                    m_listener = new LevelListener();
                    m_listener.callBack = new LevelListener.OnLevelLoaded(OnLevelLoaded);
                    m_maskAnimation.CrossFade(m_switchingClipName);

                    m_loadingP += 10;

                    if (m_stateParam.bAdditive)  //两个场景重叠
                        App.LevelMgr.LoadLevelAdditiveAsync(m_stateParam.TargetSceneName, m_listener);
                    else
                        App.LevelMgr.LoadLevelAsync(m_stateParam.TargetSceneName, m_listener);
                }
                else
                {
                    m_doneLoaded = true;
                }
            }
            else if (m_doneLoaded && !m_startedFadeOut)
            {
                App.Game.GameStateMgr.SwitchTo(m_stateParam.TargetSceneGameStateID, m_stateParam.TargetSceneParam);
                this.m_startedFadeOut = true;

                this.m_loadingP += 10;
            }
        }
    }

    private void OnLevelLoaded(string sceneName)
    {
        if (sceneName.Equals(m_stateParam.TargetSceneName))
            m_doneLoaded = true;

        if (m_bManualClose)
        {
            m_loadingP = m_levelLoadingThreshold;
        }
        else
        {
            m_loadingP = m_totalLoadingP;
        }
    }

    private void ShowDarkMask(bool bShow)
    {
        if (Camera.main != null)
        {
            Transform t = Camera.main.transform.FindChild("DarkMask");
            if (t != null && t.gameObject.renderer != null)
            {
                if (bShow)
                {
                    //iTween.FadeFrom(t.gameObject, iTween.Hash("alpha", 1, "time", 0));
                }
                else
                {
                    //iTween.FadeTo(t.gameObject, iTween.Hash("alpha", 0, "time", 0.5f));
                }
            }
        }
    }
}