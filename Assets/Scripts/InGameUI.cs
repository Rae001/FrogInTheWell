using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    
    [SerializeField] GameObject pauseUI; // ���� �Ͻ����� ��ư
    [SerializeField] Transform playerPos; // �÷��̾��� ����

    //==================================================================================
    public void Pause()
    {
        
        Time.timeScale = 0; // ������ �Ͻ�����

        pauseUI.gameObject.SetActive(true); // �Ͻ�����ȭ�� Ȱ��ȭ
    }

    public void TQuit()
    {

        // ���� �÷��̾��� ��ġ ���� Player Ŭ������ �Ҵ�
        DataManager.Instance.nowPlayer.playerX = playerPos.position.x;
        DataManager.Instance.nowPlayer.playerY = playerPos.position.y;
        
        DataManager.Instance.SaveData(); // ������ ����
        
        Time.timeScale = 1; // �Ͻ����� ���� : Time.timeScale�� 1�� �����ϴ� ������ Time.timeScale�� 0���� ���� �� ����ȭ������� �̵��ϰ� �ٽ� ���Ӿ����� ���ƿ��� �� ������ �������� ����
    
        SceneManager.LoadScene(0); // ����ȭ������� �̵�
    }

    public void FQuit()
    {
        Time.timeScale = 1;
        pauseUI.gameObject.SetActive(false);
    }
}
