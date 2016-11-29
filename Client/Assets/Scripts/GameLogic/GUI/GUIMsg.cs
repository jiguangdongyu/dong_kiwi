using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// GUI消息体
/// </summary>
public class GUIMsg
{
    private bool m_needLockSend;//消息发送完毕前不允许发送其他消息

    public int ID { get; set; }
    public bool NeedLockSend
    {
        get
        {
            return m_needLockSend;
        }
        set
        {
            m_needLockSend = value;
        }
    }

    public GUIMsg(string name)
    {
        ID = KWUtils.String2Hash(name);
    }

    public virtual void Execute(Dictionary<int, Delegate> handlerDic)
    {
        if (handlerDic.ContainsKey(ID))
        {
            ((GUIMsgMgr.GUIMsgHandler)handlerDic[ID])(this);
        }
        else
        {
            Logger.LogWarning("unknow msg: " + ID);
        }
    }
}
