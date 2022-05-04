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
        LoadLevel("Ryan's Level 1-1");
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

}
