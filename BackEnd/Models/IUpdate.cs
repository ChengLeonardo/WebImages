using System.Numerics;

namespace BackEnd.Models;

public interface IUpdate<T>
{
    void Update(T objeto);
}