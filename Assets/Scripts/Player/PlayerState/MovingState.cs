using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MovingState : BaseState
{
    // bắt buộc phải có obj và state machine để hoạt động 
    public PlayerInput playerInput;
    private GameObject clickEffect;

    public override void Enter()
    {
        playerInput = PlayerInput.instance; // lấy scripts PlayerInput
        clickEffect = playerInput.clickEffect;

        // Phát animation 
        playerInput.Anim.SetBool("isMoving", true);

        // tạo Ray để lưu trữ ray khi người dùng Click 
        Ray ray = playerInput.InputClickPoint();
        // tạo một biến RaycastHit để lưu vị trí hit với WorldSpace
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            playerInput.Agent.SetDestination(hit.point);

            clickEffect.SetActive(true); // bật gameobj lên
            clickEffect.transform.position = hit.point + new Vector3(0, .5f, 0);

            // Tính toán hướng quay
            Vector3 direction = (hit.point - playerInput.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            playerInput.transform.rotation = targetRotation;
        }
    }

    public override void Execute()
    {
        // thực hiện việc di chuyển và phát hình ảnh 
        if (playerInput.Agent.remainingDistance < 0.2f)
        {
            stateMachine.ChangeState(new IdleState());
            clickEffect.SetActive(false); // tắt đi hiệu ứng chuột trước khi chuyển sang state khác
        }
    }

    public override void Exit()
    {
        // phát animtion 
        playerInput.Anim.SetBool("isMoving", false);
    }
}
