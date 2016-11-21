using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesMgr
{
    //ansyc
    private Dictionary<string, ResourceRequest> m_requests = new Dictionary<string, ResourceRequest>();
    private Dictionary<string, List<ResListener>> m_listeners = new Dictionary<string, List<ResListener>>();
    private List<string> m_removeList = new List<string>();

    private Dictionary<string, UnityEngine.Object> m_resObjectDic = new Dictionary<string, UnityEngine.Object>();

    public void ClearAllCacheAsset()
    {
        m_listeners.Clear();
        m_requests.Clear();
        m_removeList.Clear();
        m_resObjectDic.Clear();
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }

    public void Start()
    {

    }

    /// <summary>
    /// 异步获取资源
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="listener"></param>
    public void GetAssetFromResourceAsync(string resName, ResListener listener)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Logger.LogWarning("res name is null");
        }
        else if (listener == null)
        {
            Logger.LogWarning("listener is null");
        }
        else
        {
            UnityEngine.Object obj = TryGetResObject(resName);
            if (obj != null)
            {
                listener.callback(obj, listener.param);
            }
            else
            {
                GetResAnsyc(resName, listener);
            }
        }
    }

    /// <summary>
    /// 同步获取资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object GetAssetFromResources(string resName)
    {
        UnityEngine.Object ret = null;
        if (string.IsNullOrEmpty(resName))
        {
            Logger.LogWarning("res name is null");
        }
        else
        {
            ret = TryGetResObject(resName);
            if (ret != null)
            {
                return ret;
            }
            else
            {
                ret = LoadRes(resName);
                AddResObject(resName, ret);
                return ret;
            }
        }
        return ret;
    }

    /// <summary>
    /// 从缓存中获取资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public UnityEngine.Object GetCachedAssetFromResources(string resName)
    {
        return TryGetResObject(resName);
    }

    public UnityEngine.Object LoadRes(string resName)
    {
        return Resources.Load(resName);
    }

    private void GetResAnsyc(string resName, ResListener listener)
    {
        if (!m_requests.ContainsKey(resName))
        {
            m_requests.Add(resName, Resources.LoadAsync(resName));
        }

        List<ResListener> list;
        this.m_listeners.TryGetValue(resName, out list);
        if (list == null)
        {
            list = new List<ResListener>();
            m_listeners.Add(resName, list);
        }
        list.Add(listener);
    }

    internal UnityEngine.Object TryGetResObject(string resName)
    {
        if (m_resObjectDic.ContainsKey(resName))
        {
            return m_resObjectDic[resName];
        }
        return null;
    }

    private void AddResObject(string resName, UnityEngine.Object obj)
    {
        if (m_resObjectDic.ContainsKey(resName))
        {
            //Logger.LogWarning("存在同名资源:" + resName);
            return;
        }

        m_resObjectDic.Add(resName, obj);
    }

    public void Update()
    {
        if (this.m_requests.Count > 0)
        {
            m_removeList.Clear();
            foreach (KeyValuePair<string,ResourceRequest> pair in this.m_requests)
            {
                if (pair.Value.isDone)
                {
                    string key = pair.Key;
                    m_removeList.Add(key);
                    if (m_listeners.ContainsKey(key))
                    {
                        List<ResListener> list = m_listeners[key];
                        foreach (ResListener listener in list)
                        {
                            try
                            {
                                this.AddResObject(pair.Key, pair.Value.asset);
                                listener.callback(pair.Value.asset, listener.param);
                            }
                            catch (System.Exception ex)
                            {
                                Logger.LogError("ResourceMgr Exception: " + ex.Message + " " + ex.StackTrace);
                            }
                        }
                    }
                }
            }

            if (m_removeList.Count > 0)
            {
                foreach (string str in m_removeList)
                {
                    m_listeners.Remove(str);
                    m_requests.Remove(str);
                }
            }
        }
    }
}