using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    public Vector3 displacement;

    private void Update()
    {
        transform.position = new Vector3(player.position.x + displacement.x, displacement.y, displacement.z);
    }

}
