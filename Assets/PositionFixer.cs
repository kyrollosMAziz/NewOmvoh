using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFixer : MonoBehaviour
{
    public Transform player;
    public float offset = 1.416548f;
    public float zoffset;


    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y - offset, player.position.z - zoffset);
    }
}
