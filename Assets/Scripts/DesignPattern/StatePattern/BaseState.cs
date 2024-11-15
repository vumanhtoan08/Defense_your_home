public abstract class BaseState
{
    // để thêm Class StateMachine 
    public StateMachine stateMachine;

    // 3 trạng thái của State
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
