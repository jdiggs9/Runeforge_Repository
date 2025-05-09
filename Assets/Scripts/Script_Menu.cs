using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Quit()
    {
        //Application.Quit();
        Debug.Log("Player Quit");
    }
    public void Lv1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Lv2()
    {
        SceneManager.LoadScene("TestLevel");
    }
    public void Lv3()
    {
        SceneManager.LoadScene("TitleScrene");
    }
}
