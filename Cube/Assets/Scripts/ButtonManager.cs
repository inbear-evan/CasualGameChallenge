using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject soundPanel;
    Animator popupAnim;
    public Image pauseMenu;
    [SerializeField] GameObject settingPanel;
    

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(false);
        }
        if (settingPanel != null)
        {
            settingPanel.gameObject.SetActive(false);
        }
        popupAnim = soundPanel.GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Pause Menu Button
    /// </summary>
    public void OnPauseClick()
    {
        //Time.timeScale = 0;
        GameManager.instance.isPause = true;
        pauseMenu.gameObject.SetActive(true);
    }
    public void OnClickExitPause()
    {
        //Time.timeScale = 1;
        GameManager.instance.isPause = false;
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
        soundPanel.SetActive(true);
        popupAnim.SetTrigger("popup");
        //StartCoroutine(nameof(PopupAnim));
    }
        bool IsPopup = false;
    public void PopupAnim()
    {
        if (!IsPopup)
        {
        popupAnim.SetTrigger("popup");
        IsPopup = true;
        Debug.Log("소리설정");
            return;
        }
        IsPopup = false;
        popupAnim.SetTrigger("popdown");
    }
    public void onClickSaveMusic()
    {
        Debug.Log("소리 저장");
    }
    public void onClickGameStart()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void onClickSetting()
    {
        settingPanel.SetActive(true);
    }

    public void onClickExitSetting()
    {
        settingPanel.SetActive(false);

    }

}