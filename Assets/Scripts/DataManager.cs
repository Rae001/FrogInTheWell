using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    // �÷��̾��� ��ġ���� ���� ������
    public float playerX;
    public float playerY;
}


public class DataManager : MonoBehaviour
{
    // �̱���=================================================
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

    public string path; // ���
    void Awake()
    {
        #region Singleton
        if (instance == null) // instance�� null�� ���
        {
            instance = this; // �Ҵ�

            DontDestroyOnLoad(this.gameObject); // ����ȯ�� �Ǿ �ı����� �ʵ��� ����
        }
        else // �̹� �Ҵ� �Ǿ��ִ°�� 
        { 
            Destroy(this.gameObject); // �ı�
        }
        #endregion

        path = Application.persistentDataPath + "/save"; //���� ������ ���͸��� ���� ��θ� ����. �� ���� ���� ���� �����Ϸ��� �����͸� ������ �� �ִ� ���͸� ���.
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
