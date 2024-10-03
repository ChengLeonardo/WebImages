using System.Numerics;

namespace BackEnd.Interface;

public interface IUpdate<T>
{
    void Update(T objeto);
}