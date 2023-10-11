using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
   
    [SerializeField] GameObject newGameUI; // 새로하기 재확인 버튼
    [SerializeField] GameObject loadGameUI; // 이어하기 버튼 
    [SerializeField] GameObject exitUI; //종료 재확인 버튼

    // 게임 씬 전환 시 페이드 인 & 아웃 효과를 적용
    //===================================================================================


    #region OnClick_LoadGameButton
    public void LoadGame() // 이어하기 버튼
    {
        DataManager.Instance.LoadData(); // 데이터를 불러오고
        SceneManager.LoadScene(1); // 게임씬으로 이동
    }
    #endregion

    #region OnClick_NewGameButton
    public void NewGame() // 새로하기 버튼
    {
        if (File.Exists(DataManager.Instance.path)) // 데이터가 존재한다면
        {
            newGameUI.gameObject.SetActive(true); // 새로하기 재확인 UI 활성화
        }
        else  // 데이터가 존재하지 않는다면
        {
            SceneManager.LoadScene(1); // 게임씬으로 이동
        }
    }

    public void TNewGame()
    {
        DataManager.Instance.DataClear(); // 기존 데이터 삭제후
        SceneManager.LoadScene(1); // 게임씬으로 이동
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
