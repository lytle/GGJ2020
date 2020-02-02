using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenterNew : MonoBehaviour
{
    public Transform player1, player2;

    void Start()
    {
        
    }
    void Update()
    {
        transform.position = (player1.position + player2.position) / 2;
    }
}
