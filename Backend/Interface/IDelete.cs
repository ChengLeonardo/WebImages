using System.Numerics;

namespace BackEnd.Interface;

public interface IDelete<T>
{
    void Delete(T objeto);
}