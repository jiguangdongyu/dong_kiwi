using UnityEngine;
using System.Collections;

/// <summary>
/// UI控制层级基类
/// </summary>
public class GUIFrame
{
    /// <summary>
    /// UI
    /// </summary>
    public GameObject Root
    {
        get
        {
            return m_root;
        }
        set
        {
            if (m_root != value)
            {
                if (m_root != null)
                {
                    UnityEngine.Object.Destroy(m_root);
                }
                m_root = value;
                if (m_root != null)
                {
                    UIRoot component = m_root.GetComponent<UIRoot>();
                    if (((component != null) && (component.scalingStyle == UIRoot.Scaling.PixelPerfect)) && (component.manualHeight == 0x2e0))//unuse
                    {
                        component.scalingStyle = UIRoot.Scaling.FixedSize;
                        component.manualHeight = 640;
                    }

                    if (component == null)
                    {
                        //该UI没有Camera，则使用UIRoot的相机
                        GameObject uiRoot = GameObject.Find("UI Root");
                        if (uiRoot != null)
                        {
                            UnityEngine.Camera cam = uiRoot.GetComponent<UnityEngine.Camera>();
                            cam.depth = 0;
                        }

                        UnityEngine.Camera cameraInChildren = m_root.GetComponentInChildren<UnityEngine.Camera>();
                        if (cameraInChildren == null)
                        {
                            cameraInChildren = uiRoot.GetComponentInChildren<UnityEngine.Camera>();
                        }

                        if (null != cameraInChildren)
                        {
                            m_myCamera = cameraInChildren;
                            cameraInChildren.farClipPlane = 2f;
                            cameraInChildren.nearClipPlane = -12f;
                            /*
                            if (this.haveShowAnimation && (this.aniScript == null))
                            {
                                this.aniScript = this.root.AddComponent<GUIFadeAnim>();
                                this.aniScript.SetFrame(this);
                            }
                             * */
                        }

                        //添加UI到UIRoot
                        m_root.transform.parent = uiRoot.transform;
                        m_root.transform.localScale = Vector3.one;
                        m_root.transform.localRotation = Quaternion.identity;
                        m_root.transform.localPosition = Vector3.zero;
                    }
                }
                NGUITools.SetActive(m_root, m_active);
            }
        }
    }

    protected bool m_active;                        //UI是否激活
    protected GameObject m_root;                    //UI
    protected int m_id;
    private UnityEngine.Camera m_myCamera;          //UI相机                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      

    public int ID
    {
        get
        {
            return m_id;
        }
    }

    public GUIFrameMgr Owner { get; set; }

    public GUIFrame(int id)
    {
        this.m_id = id;
    }

    public virtual void Active()
    {

    }

    public void CallWapperFunction(string funName, object param = null)
    {

    }

    public void DeActive(bool bForce = false)
    {

    }

    public void TestLoad()
    {
        if (this.m_root == null)
        {

        }
    }

    public void LoadPrefab()
    {

    }

    public void InitiateObj()
    {

    }

    public void RegisterHandles()
    {

    }

    public void Release()
    {
        DeActive();
        if (m_root != null)
        {
            UnityEngine.Object.Destroy(m_root);
            m_root = null;
        }
    }
}
