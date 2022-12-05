using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCameraFollow : MonoBehaviour
{
    [SerializeField] PlayerController _player;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * _player.SpeedForZ, Space.World);
    }
}
