using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Git Test
    public GameObject winpanelobj;
    public GameObject losepanel;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Win()
    {
        winpanelobj.SetActive(true);
        Debug.Log("win");
        Time.timeScale = 0;
    }

    public void Lose()
    {
        losepanel.SetActive(true);
        Debug.Log("lose");
        Time.timeScale = 0;
    }

    public void replay()
    {
        SceneManager.LoadScene("play");
    }
}
