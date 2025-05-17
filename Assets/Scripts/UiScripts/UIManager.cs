using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Main Menu")]
    public Button starBtn;
    public Button creditsBtn;
    public Button quitBtn;

    [Header("Pause Menu")]
    public Button resumeBtn;
    public Button quitToMainMenuBtn;
    public Button quitBttn;
    //credits icin bu
    public Button quitToMenuBtn;
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public GameObject pauseMenuPanel;


    public bool IsGamePaused = false;

    void Awake()
    {
       
    }

    void Start()
    {
        if (starBtn != null)
            starBtn.onClick.AddListener(StartGame);
        if (creditsBtn != null)
            creditsBtn.onClick.AddListener(OpenCredits);
        if (quitBtn != null)
            quitBtn.onClick.AddListener(QuitGame);
        if (resumeBtn != null)
                resumeBtn.onClick.AddListener(StartGame);
        if (quitToMainMenuBtn != null)
                quitToMainMenuBtn.onClick.AddListener(QuitToMainMenu);
        if (quitToMenuBtn != null)
            quitToMenuBtn.onClick.AddListener(QuitToMainMenu);
        if (quitBttn != null)
            quitBttn.onClick.AddListener(QuitGame);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel != null && pauseMenuPanel.activeSelf)
            {
                StartGame();
            }
            else if (creditsPanel != null && creditsPanel.activeSelf)
            {
                QuitToMainMenu();
            }
            else if (mainMenuPanel != null && !mainMenuPanel.activeSelf)
            {
                ShowPauseMenu();
            }
        }
    }



    public void StartGame()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OpenCredits()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(true);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        Time.timeScale = 0f;
    }
    public void ShowPauseMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void QuitToMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
