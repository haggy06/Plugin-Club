using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [SerializeField]
    private GameData gameData;
    public GameData DATA => gameData;

    private void Awake()
    {
        base.Singletoning();
    }
}

[System.Serializable]
public class GameData
{
    public string lastScene = "None";
    public string saveScene = "None";

    public int maxHP = 100;
    public int currentHP = 100;

    #region 青己甸 努府绢 咯何
    [Space(5), Header("Planets's Clear Status"), Space(5)]
    public bool isPlanet01Cleared = false;
    public bool isPlanet02Cleared = false;
    public bool isPlanet03Cleared = false;
    public bool isPlanet04Cleared = false;
    public bool isPlanet05Cleared = false;
#endregion
}
