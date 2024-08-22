using System.Numerics;

namespace BackEnd.Models;

public interface IDelete<T>
{
    void Delete(T objeto);
}