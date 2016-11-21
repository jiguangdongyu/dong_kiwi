using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
    public string sceneName;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnClick()
    {
        LevelListener ll = new LevelListener();
        ll.callBack = TT;
        App.LevelMgr.LoadLevelAsync(sceneName, ll);
    }

    void TT(string name)
    {
        Logger.LogYellow(name);
    }
}
