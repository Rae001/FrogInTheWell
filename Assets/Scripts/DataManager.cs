using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    // �÷��̾��� ��ġ���� ���� ������
    public float playerX = 0.14f;
    public float playerY = -4.58f;
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
        string data = JsonUtility.ToJson(nowPlayer); // ��ü�� ���� �ʵ忡 ���� JSON �������� ����ȭ
        File.WriteAllText(path, data); // path ������ �����ϰ� ���Ͽ� ������ �ۼ��Ѵ�.
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path); // path ������ �о����� JSON�� data�� �Ҵ�
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); // data�� �ٽ� ������Ʋ ��ȯ�Ͽ� �Ҵ�
    }
    public void DataClear()
    {
        nowPlayer.playerX = 0.14f;
        nowPlayer.playerY = -4.56f;
    }

}
