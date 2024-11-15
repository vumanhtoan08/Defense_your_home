using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMotor : MonoBehaviour
{
    private CharacterController camController;
    private Vector3 camPos;
    public float speed = 5f; // tốc độ của cam 
    public float speedZoom = 10f;

    public float minX = -25f; 
    public float maxX = 25f;  
    public float minZ = -25f; 
    public float maxZ = -5f;

    public float zoomSmoothTime = 0.1f; // Adjust this to control the zoom smoothness
    public float maxHeightZoom = 30f;
    public float minHeightZoom = 15f;

    private float targetZoom;
    private float currentZoom;


    // Start is called before the first frame update
    void Start()
    {
        camController = GetComponent<CharacterController>();
        currentZoom = camController.transform.localPosition.z;
        targetZoom = currentZoom;
    }

    // Update is called once per frame
    void Update()
    {
        camPos = transform.position;
    }

    public void ProcessCamMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero; // khởi tạo ra một biến lưu trữ hướng đi của Cam
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        Quaternion rotation = Quaternion.Euler(-50, 0, 0); // Tạo góc quay -50 độ trên trục X
        moveDirection = rotation * moveDirection; // Áp dụng góc quay vào hướng di chuyển

        camController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        currentPosition.z = Mathf.Clamp(currentPosition.z, minZ, maxZ);

        transform.position = currentPosition;
    }

    public void ProcessCamZoom(float input)
    {
        if (input > 0f && camPos.y > minHeightZoom)
        {
            camController.Move(transform.TransformDirection(Vector3.forward) * speedZoom * Time.deltaTime);
        }
        if (input < 0f && camPos.y < maxHeightZoom)
        {
            camController.Move(transform.TransformDirection(-Vector3.forward) * speedZoom * Time.deltaTime);
        }
    }
}
