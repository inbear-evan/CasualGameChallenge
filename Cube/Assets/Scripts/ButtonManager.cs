using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Image pauseMenu;
    private void Start()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    /// <summary>
    /// Pause Menu Button
    /// </summary>
    public void OnPauseClick()
    {
        Time.timeScale = 0;
        pauseMenu.gameObject.SetActive(true);
    }
    public void OnClickExitPause()
    {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive(false);
    }
    public void OnClickRestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnClickStartMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void onClickMusic()
    {
        Debug.Log("소리설정");
    }
    public void onClickSaveMusic()
    {
        Debug.Log("소리 저장");
    }
    public void onClickGameStart()
    {
        Debug.Log("게임 시작");
    }


}