using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x + 10, 0, -10);
    }

}
