namespace Kakky
{
    public interface IState
    {
        void OnEnter(GameDataContext context);
        void OnUpdate(GameDataContext context);
        void OnExit(GameDataContext context);
    }
}