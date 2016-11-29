using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// frame，frame资源关联
/// </summary>
public class GUIFrameBuilder
{
    private Dictionary<int, FrameAssetInfo> assetDic = new Dictionary<int, FrameAssetInfo>();

    public void BuildGuiFrame<T>(int id) where T : GUIFrame
    {
    }


    public class FrameAssetInfo
    {
        public string asset;

        public FrameAssetInfo(string asset)
        {
            this.asset = asset;
        }
    }
}
