public abstract class BaseStateEnemy 
{
    public Enemy enemy;
    public StateMachineEnemy stateMachineEnemy;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
