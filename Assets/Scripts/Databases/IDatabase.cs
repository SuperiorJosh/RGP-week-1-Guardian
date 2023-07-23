using System.Collections.Generic;

public interface IDatabase<T>
{
    public void Init();
    public void GetInstance(T data);
    public IEnumerable<T> GetAll();
    public T GetSingle();
}