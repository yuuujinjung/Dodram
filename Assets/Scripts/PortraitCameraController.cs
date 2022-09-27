using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitCameraController : MonoBehaviour
{
    public float cameraSpeed = 50.0f;

    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        dir.y += 0.5f;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
