using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    [SerializeField]
    private GameData gameData;
    public GameData DATA => gameData;

    //public static GameDataManager gameDataManager; 
    /*
    private void Awake()
    {
        if (gameDataManager != null)
        {
            Debug.LogWarning("한 하늘 아래 2개 이상의 GameDataManger가 존재할 수 없습니다.");
            Destroy(this);
        }
        else
        {
            gameDataManager = this;
            DontDestroyOnLoad(this);
        }
    }*/
}

[System.Serializable]
public class GameData
{
    public string lastScene = "None";
    public string saveScene = "None";

    public int maxHP = 100;
    public int currentHP = 100;

    #region 행성들 클리어 여부
    [Space(5), Header("Planets's Clear Status"), Space(5)]
    public bool isPlanet01Cleared = false;
    public bool isPlanet02Cleared = false;
    public bool isPlanet03Cleared = false;
    public bool isPlanet04Cleared = false;
    public bool isPlanet05Cleared = false;
#endregion
}
