using UnityEngine;
using System.Collections;

/// <summary>
/// 场景工厂
/// </summary>
public class GameStateFactory
{
    public static void Build(GameStateMgr gsm)
    {
        gsm.Register(new BootState(GameStateID.ST_BOOT));
    }
}
