using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadWhenVidoeOver : MonoBehaviour
{

    [SerializeField] VideoPlayer VidPlayer;
    [SerializeField] string NextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        VidPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!VidPlayer.isPlaying)
        {
            SceneManager.LoadSceneAsync(NextSceneName);
        }
    }
}
