public interface IDatabase<T>
{
    public void Init();
    public void GetInstance(T data);
}