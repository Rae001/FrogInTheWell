using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    // 플레이어의 위치값을 담을 변수들
    public float playerX;
    public float playerY;
}


public class DataManager : MonoBehaviour
{
    // 싱글톤=================================================
    private static DataManager instance;
    public PlayerData nowPlayer = new PlayerData();
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    //=========================================================

    public string path; // 경로
    void Awake()
    {
        #region Singleton
        if (instance == null) // instance가 null일 경우
        {
            instance = this; // 할당

            DontDestroyOnLoad(this.gameObject); // 씬전환에 되어도 파괴되지 않도록 설정
        }
        else // 이미 할당 되어있는경우 
        { 
            Destroy(this.gameObject); // 파괴
        }
        #endregion

        path = Application.persistentDataPath + "/save"; //영구 데이터 디렉터리에 대한 경로를 포함. 이 값은 실행 간에 보관하려는 데이터를 저장할 수 있는 디렉터리 경로.
        print(path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        print(nowPlayer.playerX);
        print(nowPlayer.playerY);
    }
    public void DataClear()
    {
        nowPlayer.playerX = 0.14f;
        nowPlayer.playerY = -4.56f;
    }

}
