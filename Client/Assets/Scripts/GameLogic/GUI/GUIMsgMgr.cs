using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// GUI消息管理器
/// </summary>
public class GUIMsgMgr
{
    public delegate void GUIMsgHandler(GUIMsg msg);

    private Dictionary<int, Delegate> m_handlerDic = new Dictionary<int, Delegate>();
    private bool m_lockMessageQueue;
    private Queue<GUIMsg> m_msgs = new Queue<GUIMsg>(); //对象的先进先出集合，存储在 Queue（队列） 中的对象在一端插入，从另一端移除。
    private float m_timeCounter = 0;
    private float m_minMsgIntervalTime = 0.2f;

    public void HandleMsg(GUIMsg msg)
    {
        if (msg != null)
        {
            msg.Execute(m_handlerDic);
        }
    }

    public void RegisterHandler(int id, GUIMsgHandler handler)
    {
        if (handler == null)
        {
            Logger.LogWarning("handler is null");
        }
        else
        {
            if (!m_handlerDic.ContainsKey(id))
            {
                m_handlerDic.Add(id, null);
            }
            m_handlerDic[id] = (GUIMsgHandler)Delegate.Combine((GUIMsgHandler)m_handlerDic[id], handler);
        }
    }

    public void SendMsg(GUIMsg msg)
    {
        if ((msg != null) && (!msg.NeedLockSend || !m_lockMessageQueue))
        {
            m_msgs.Enqueue(msg);
            m_lockMessageQueue = true;
        }
    }

    public void ShutDown()
    {
        m_msgs.Clear();
        m_handlerDic.Clear();
    }

    public void Start()
    {

    }

    public void UnRegisterHandler(int id, GUIMsgHandler handler)
    {
        if ((handler != null) && m_handlerDic.ContainsKey(id))
        {
            m_handlerDic[id] = (GUIMsgHandler)Delegate.Remove((GUIMsgHandler)m_handlerDic[id], handler);
            if (m_handlerDic[id] == null)
            {
                m_handlerDic.Remove(id);
            }
        }
    }

    public void Update()
    {
        if (m_lockMessageQueue)
        {
            m_timeCounter += Time.deltaTime;
            if (m_timeCounter > m_minMsgIntervalTime)
            {
                m_timeCounter = 0;
                m_lockMessageQueue = false;
            }
        }

        while(m_msgs.Count > 0)
        {
            GUIMsg msg = m_msgs.Dequeue();
            HandleMsg(msg);
        }
    }
}
