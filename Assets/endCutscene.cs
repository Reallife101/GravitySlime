using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endCutscene : MonoBehaviour
{
    [SerializeField] string sc;
    [SerializeField] float cutSceneLength;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(endScene(cutSceneLength));
    }

    private IEnumerator endScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sc);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(sc);
        }
    }
}
