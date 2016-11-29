using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// UI控制层管理
/// </summary>
public class GUIFrameMgr
{
    private List<int> m_activeList = new List<int>();
    private List<int> m_activeQueue = new List<int>();
    private List<int> m_deActiveQueue = new List<int>();
    private GUIFrameBuilder frameBuilder = new GUIFrameBuilder();
    private Dictionary<int, GUIFrame> m_frameDic = new Dictionary<int, GUIFrame>();
    private GUIMsgMgr m_msgMgr = new GUIMsgMgr();

    public void Active(int id)
    {
        GUIFrame frame = GetFrame(id);
        if (frame == null)
        {
            Logger.LogWarning("not register frame: " + id);
            return;
        }

        if (m_deActiveQueue.Contains(id))
        {
            m_frameDic[id].DeActive();
            m_deActiveQueue.Remove(id);
            m_activeList.Remove(id);
        }
        if (!m_activeList.Contains(id) && m_activeQueue.Contains(id))
        {
            m_activeQueue.Add(id);
            frame.TestLoad();
        }
    }

    public void DeActive(int id, bool forceWithOutAni = false)
    {
        GUIFrame frame = GetFrame(id);
        //show Ani todo
        if (frame == null)
        {
            Logger.Log("not registered frame " + id);
        }
        else if (!m_activeList.Contains(id))
        {
            if (m_activeQueue.Contains(id))
            {
                m_activeQueue.Remove(id);
            }
        }
        else if (!m_deActiveQueue.Contains(id))
        {
            m_deActiveQueue.Add(id);
        }
    }

    public bool IsActive(int id)
    {
        return m_activeList.Contains(id) || m_activeQueue.Contains(id);
    }


    public GUIFrame GetFrame(int id)
    {
        if (!m_frameDic.ContainsKey(id))
        {
            return null;
        }
        return m_frameDic[id];
    }

    public void Register(GUIFrame frame)
    {
        if (frame != null && !m_frameDic.ContainsKey(frame.ID))
        {
            frame.Owner = this;
            frame.RegisterHandles();
            m_frameDic.Add(frame.ID, frame);
        }
    }

    public void ShutDown()
    {
        foreach (int num in m_activeList)
        {
            m_frameDic[num].DeActive();
        }
        m_activeList.Clear();

        foreach (GUIFrame frame in m_frameDic.Values)
        {
            frame.Release();
            m_msgMgr.ShutDown();
        }
    }

    public void Start()
    {
        m_msgMgr.Start();
    }

    private void SyncActives()
    {
        bool flag = false;
        if (m_activeQueue.Count > 0)
        {
            List<int> list = new List<int>(m_activeQueue);
            int topDepth = 0;
            if (m_activeList.Count > 0)
            {
                foreach (int frameId in m_activeList)
                {

                }
            }
        }
    }
}
