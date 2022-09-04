using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody characterRigidbody;
    private Vector3 movement;
    private Vector3 heading;

    private Transform mainCamera;



    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        mainCamera = GameObject.Find("Main Camera").transform;
        heading = mainCamera.localRotation * Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MoveCharactor();
    }

    private void MoveCharactor()
    {
        heading.y = 0;
        heading = heading.normalized;


        //정면이동 대입연산
        movement = heading * Time.deltaTime * Input.GetAxis("Vertical");
        //측면이동 합연산
        movement += Quaternion.Euler(0, 90, 0) * heading * Time.deltaTime * Input.GetAxis("Horizontal");

        transform.Translate(movement*speed);

    }

}
