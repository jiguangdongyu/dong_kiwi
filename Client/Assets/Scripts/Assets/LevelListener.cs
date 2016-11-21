using UnityEngine;
using System.Collections;

/************************************************************************/
/*      author: ljdong 
 *      date:   2016/11/18        
 *      desc:   场景监听器，场景资源加载完毕，则触发相关函数
/************************************************************************/
public class LevelListener
{
    public delegate void OnLevelLoaded(string sceneName);
    public OnLevelLoaded callBack;
}
