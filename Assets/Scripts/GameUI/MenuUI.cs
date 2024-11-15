using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public static MenuUI instance;

    public GameObject chooseLevelScreen;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ShowChooseLevelScreen()
    {
        chooseLevelScreen.SetActive(true);
    }
    public void HideChooseLevelScreen()
    {
        chooseLevelScreen.SetActive(false);
    }


}
