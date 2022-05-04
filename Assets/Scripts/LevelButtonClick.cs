using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonClick : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(this.name);
    }

}
