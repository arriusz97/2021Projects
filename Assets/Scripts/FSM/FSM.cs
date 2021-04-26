using UnityEngine;

public class FSM <T> : MonoBehaviour
{
    //상태 관리자
    private T manager;
    //현재 상태
    private IFSMState<T> currentState = null;
    //이전 상태
    private IFSMState<T> previousState = null;

    //현재 상태(읽기 전용)
    public IFSMState<T> CurrentState { get { return currentState; } }
    //이전 상태(읽기 전용)
    public IFSMState<T> PreviousState { get { return previousState; } }

    protected void InitState(T manager, IFSMState<T> initialState)
    {
        this.manager = manager;
        //initial state로 첫 상태 설정
        ChangeState(initialState);
    }

    //현재 상태의 실시간 진행처리
    protected void FSMUpdate()
    {
        if (currentState != null)
            currentState.Execute(manager);
    }

    
    //상태 변경
    //기존 상태 -> new state
    public void ChangeState(IFSMState<T> newState)
    {
        //현재 상태 종료 처리
        if (currentState != null)
            currentState.Exit(manager);

        //기존상태를 이전 상태로 설정, 현재 상태를 newstate로 설정
        previousState = currentState;
        currentState = newState;

        if (currentState != null)
            currentState.Enter(manager);
    }


    //이전 상태로 전환
    public void RevertState()
    {
        //previousState로 변경
        if (previousState != null)
            ChangeState(previousState);
    }

    //현재 상태를 문자열로 전환
    public override string ToString()
    {
        return currentState.ToString();
    }
}
