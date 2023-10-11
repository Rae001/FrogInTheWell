using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    
    [SerializeField] GameObject pauseUI; // 게임 일시정지 버튼
    [SerializeField] Transform playerPos; // 플레이어의 정보

    //==================================================================================
    public void Pause()
    {
        
        Time.timeScale = 0; // 게임을 일시정지

        pauseUI.gameObject.SetActive(true); // 일시정지화면 활성화
    }

    public void TQuit()
    {

        // 현재 플레이어의 위치 값을 Player 클래스에 할당
        DataManager.Instance.nowPlayer.playerX = playerPos.position.x;
        DataManager.Instance.nowPlayer.playerY = playerPos.position.y;
        
        DataManager.Instance.SaveData(); // 데이터 저장
        
        Time.timeScale = 1; // 일시정지 해제 : Time.timeScale을 1로 변경하는 이유는 Time.timeScale을 0으로 변경 후 메인화면씬으로 이동하고 다시 게임씬으로 돌아왔을 때 게임이 동작하지 않음
    
        SceneManager.LoadScene(0); // 메인화면씬으로 이동
    }

    public void FQuit()
    {
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
    }
}
