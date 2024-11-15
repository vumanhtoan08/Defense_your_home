using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // thuộc tính cho sự kiện thắng / thua của game 
    private bool isGameOver = false;
    public bool isVictory = false;
    private bool isPause = false;

    public GameObject gameOverScreen;
    public GameObject gameVictoryScreen;
    public GameObject gamePauseScreen;
    public GameObject gameSettingScreen;

    public int enemyToVictory = 20; // số lượng zombie cần để chiến thắng 
    public int currentEnemyToVictory; // số lượng zombie còn lại 
    public TextMeshProUGUI enemyRemainToVictory; 

    // script bên ngoài
    private PlayerInput playerInput;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Application.targetFrameRate = 90;
    }

    private void Start()
    {
        currentEnemyToVictory = enemyToVictory;
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    private void Update()
    {
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        
        if (isGameOver)
        {
            ShowGameOverScreen();
        }
        else
        {
            HideGameOverScreen();
        }

        if (isVictory)
        {
            ShowGameVictoryScreen();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
            {
                ShowGamePauseScreen();
            }
            else
            {
                HideGamePauseScreen();
            }
        }
    }

    #region Phương thức 
    // nếu máu castle = 0 thì thua
    public void GameOver()
    {
        isGameOver = true;
    }

    public void GameVictory()
    {
        isVictory = true;
    }
    public void GamePause()
    {
        Time.timeScale = 0f;
        playerInput.enabled = false;
    }
    public void GameResume()
    {
        isPause = false;
        Time.timeScale = 1f;
        playerInput.enabled = true;
    }
    public void PlayAgain()
    {
        isGameOver = false;
        isVictory = false;
        currentEnemyToVictory = enemyToVictory;
        enemyRemainToVictory.SetText("Enemy remain: " + currentEnemyToVictory);
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    #endregion

    #region Hiển thị UI 
    public void ShowGameOverScreen()
    {
        GamePause();
        gameOverScreen.SetActive(true);
    }
    public void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void ShowGameVictoryScreen()
    {
        GamePause();
        gameVictoryScreen.SetActive(true);
    }
    public void HideGameVictoryScreen()
    {
        gameVictoryScreen.SetActive(false);
    }

    public void ShowGamePauseScreen()
    {
        GamePause();
        gamePauseScreen.SetActive(true);
    }
    public void HideGamePauseScreen()
    {
        GameResume();
        isPause = false;
        gamePauseScreen.SetActive(false);
    }
    public void ShowGameSettingScreen()
    {
        gameSettingScreen.SetActive(true);
    }
    public void HideGameSettingScreen()
    {
        gameSettingScreen.SetActive(false);
    }
    #endregion

    public void SubEnemy() // trừ số lượng kẻ thù cho đến khi chiến thắng 
    {
        if (currentEnemyToVictory > 0)
        {
            currentEnemyToVictory--;
            Debug.Log(currentEnemyToVictory);
            enemyRemainToVictory.SetText("Enemy remain: " + currentEnemyToVictory);
        }
        if (currentEnemyToVictory == 0)
        {
            GameVictory();
        }
    }
}
