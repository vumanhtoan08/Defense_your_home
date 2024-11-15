using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public GameObject GameUIScreen; 
    // scrippt cho nút play again 
    public void PlayAgain()
    {
        GameManager.instance.PlayAgain();
        GameManager.instance.GameResume();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); 
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Chuyển đến scene tiếp theo
            SceneManager.LoadSceneAsync(nextSceneIndex);
            GameManager.instance.enemyToVictory = 5 * nextSceneIndex;
        }
        else
        {
            // Nếu không có scene tiếp theo, quay về scene đầu tiên
            SceneManager.LoadSceneAsync(0);
            GameUIScreen.SetActive(true);
        }
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex);
        GameManager.instance.enemyToVictory = 5 * buildIndex;
    }
}
