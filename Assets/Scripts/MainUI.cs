using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
   
    [SerializeField] GameObject newGameUI; // �����ϱ� ��Ȯ�� ��ư
    [SerializeField] GameObject loadGameUI; // �̾��ϱ� ��ư 
    [SerializeField] GameObject exitUI; //���� ��Ȯ�� ��ư

    // ���� �� ��ȯ �� ���̵� �� & �ƿ� ȿ���� ����
    //===================================================================================


    #region OnClick_LoadGameButton
    public void LoadGame() // �̾��ϱ� ��ư
    {
        DataManager.Instance.LoadData(); // �����͸� �ҷ�����
        SceneManager.LoadScene(1); // ���Ӿ����� �̵�
    }
    #endregion

    #region OnClick_NewGameButton
    public void NewGame() // �����ϱ� ��ư
    {
        if (File.Exists(DataManager.Instance.path)) // �����Ͱ� �����Ѵٸ�
        {
            newGameUI.gameObject.SetActive(true); // �����ϱ� ��Ȯ�� UI Ȱ��ȭ
        }
        else  // �����Ͱ� �������� �ʴ´ٸ�
        {
            SceneManager.LoadScene(1); // ���Ӿ����� �̵�
        }
    }

    public void TNewGame()
    {
        DataManager.Instance.DataClear(); // ���� ������ ������
        SceneManager.LoadScene(1); // ���Ӿ����� �̵�
    }
    public void FNewGame()
    {
        //
        newGameUI.gameObject.SetActive(false); 
    }
    #endregion

    #region OnClick_ExitButton
    public void ExitUI() 
    {
        exitUI.gameObject.SetActive(true);
    }
    public void TExit()
    {
        Application.Quit();
    }
    public void FExit()
    {
        exitUI.gameObject.SetActive(false);
    }
    #endregion
}
