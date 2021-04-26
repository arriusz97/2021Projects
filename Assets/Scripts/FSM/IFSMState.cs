//각 상태의 처리 단계를 3단계로 처리
public interface IFSMState<T>
{
    //상태 진입
    void Enter(T e);

    //상태 진행
    void Execute(T e);

    //상태 종료
    void Exit(T e);
}
