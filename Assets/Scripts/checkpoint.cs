using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{

    [SerializeField] Vector3 respawnLocation;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawnLocation = new Vector3(transform.position.x, -4, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("here");
        player.GetComponent<movement>().setRespawn(respawnLocation);
    }

}
