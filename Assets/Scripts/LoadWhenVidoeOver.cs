using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadWhenVidoeOver : MonoBehaviour
{

    [SerializeField] VideoPlayer VidPlayer;
    [SerializeField] string NextSceneName;
    private bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if(VidPlayer.isPlaying)
        {
            playing = true;
        }
        if(playing & !VidPlayer.isPlaying)
        {
            SceneManager.LoadSceneAsync(NextSceneName);
        }
    }
}
