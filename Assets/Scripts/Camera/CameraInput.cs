using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CamMotor))]
public class CameraInput : MonoBehaviour
{
    private GameInput gameInput;
    public GameInput.CamActions cam;

    private CamMotor motor;

    void Awake()
    {
        gameInput = new GameInput();
        cam = gameInput.Cam;

        motor = GetComponent<CamMotor>();
    }

    private void FixedUpdate()
    {
        motor.ProcessCamMove(cam.Move.ReadValue<Vector2>()); // thực hiện việc xử lý di chuyển
    }
    private void LateUpdate()
    {
        motor.ProcessCamZoom(cam.Zoom.ReadValue<float>()); // thực hiện việc xử lý zoom
    }

    private void OnEnable()
    {
        cam.Enable();
    }
    private void OnDisable()
    {
        cam.Disable();
    }
}
