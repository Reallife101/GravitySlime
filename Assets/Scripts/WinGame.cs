using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    [SerializeField] string NextLevel;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(NextLevel);
    }
}
