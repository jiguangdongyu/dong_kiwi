using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

/// <summary>
/// 多语言管理
/// </summary>
public class StringResMgr
{
    public Dictionary<string, StringUnit> stringDic = new Dictionary<string, StringUnit>();

    /// <summary>
    /// 本地多语言配置文件
    /// </summary>
    public void InitStringConf()
    {
        ReadDataFromFile("String/strings");
    }

    /// <summary>
    /// 从文件读取文本内容
    /// </summary>
    /// <param name="fileName"></param>
    public void ReadDataFromFile(string fileName)
    {
        TextAsset textAsset = Resources.Load(fileName) as TextAsset;

        this.AddTextFromAsset(textAsset);
        Resources.UnloadAsset(textAsset);
    }

    public void AddTextFromAsset(TextAsset textAsset, bool replaceExisting = false)
    {
        ReadDataFromAsset(textAsset, replaceExisting);
    }

    /// <summary>
    /// 读取文本流通过xml处理
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="replaceExiting"></param>
    public void ReadDataFromAsset(TextAsset asset, bool replaceExiting)
    {
        XmlDocument xmlDocument = new XmlDocument();
        if (asset == null)
        {
            Logger.LogWarning("text asset is null");
            return;
        }
        xmlDocument.LoadXml(asset.ToString());
        XmlElement documentElement = xmlDocument.DocumentElement;//根节点 <root>...</root>
        XmlNodeList elementsByTagName = documentElement.GetElementsByTagName("i");   //所有<i>..</i>节点集合
        foreach (XmlNode xmlNode in elementsByTagName)
        {
            string key = "0";
            StringUnit language = new StringUnit();
            foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)//所有子节点集合，<k>tips_1</k><zh>小提示:点击主界面头像可以查看更多功能</zh><en></en>
            {
                if (xmlNode2.Name.Equals("k"))
                {
                    if (string.IsNullOrEmpty(xmlNode2.InnerXml)) //InnerText 只是取 #Text 节点内容，而 InnerXml 则具有子节点名称、<![CDATA[]]> 等
                    {
                        key = "???";
                    }
                    else
                    {
                        key = xmlNode2.InnerXml;
                    }
                }

                if (xmlNode2.Name.Equals("zh"))
                {
                    if (string.IsNullOrEmpty(xmlNode2.InnerXml))
                    {
                        language.zh = "???";
                    }
                    else
                    {
                        language.zh = xmlNode2.InnerXml;
                    }
                }

                if (xmlNode2.Name == "tw")
                {
                    if (string.IsNullOrEmpty(xmlNode2.InnerXml))
                    {
                        language.tw = "???";
                    }
                    else
                    {
                        language.tw = xmlNode2.InnerXml;
                    }
                }

                if (xmlNode2.Name == "en")
                {
                    if (string.IsNullOrEmpty(xmlNode2.InnerXml))
                    {
                        language.en = "???";
                    }
                    else
                    {
                        language.en = xmlNode2.InnerXml;
                    }
                }

                if (replaceExiting)
                {
                    if (stringDic.ContainsKey(key))
                    {
                        stringDic[key] = language;
                    }
                    else
                    {
                        stringDic.Add(key, language);
                    }
                }
                else
                {
                    if (!stringDic.ContainsKey(key))
                    {
                        stringDic.Add(key, language);
                    }
                }
            }
        }
    }

    public string GetMutiText(string id)
    {
        StringUnit lanaguage;
        stringDic.TryGetValue(id, out lanaguage);
        if (lanaguage == null)
        {
            Logger.LogRed("string res not found : " + id);
            return "text:" + id + " do not exist";

        }
        return lanaguage.zh;
    }
}
