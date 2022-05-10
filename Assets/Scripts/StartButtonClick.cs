using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonClick : MonoBehaviour
{

    private void Start()
    {
    }
    
    public void StartGame()
    {
        LoadLevel("IntroVideoScene");
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

}
