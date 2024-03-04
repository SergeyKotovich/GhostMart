public interface IPayLoadedState<T> : IInitializable
{
    void OnEnter(T payload);
}