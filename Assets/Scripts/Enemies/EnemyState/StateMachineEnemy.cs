using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineEnemy : MonoBehaviour
{
    public BaseStateEnemy activeState;

    // Start is called before the first frame update
    public virtual void Initialise() // ham nay de set State Default 
    {
        ChangeState(new MovingStateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        // nếu đang có state hoạt động thì khởi chạy hàm Execute của state đó 
        if (activeState != null)
            activeState.Execute();
    }

    public void ChangeState(BaseStateEnemy newState)
    {
        // kiểm tra nếu có State thì thoát để chuyển sang State mới 
        if (activeState != null)
            activeState.Exit();

        // Set activeState = State mới
        activeState = newState;

        // chuyển sang trạng thái mới 
        if (activeState != null)
        {
            activeState.stateMachineEnemy = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
