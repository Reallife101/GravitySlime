using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayer : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<Animator>().SetTrigger("death");
        StartCoroutine(turnOffplayer());
        player.GetComponent<movement>().speed = 0f;
    }

    IEnumerator turnOffplayer()
    {
        yield return new WaitForSeconds(.75f);
        player.SetActive(false);
    }

}
