using System.Numerics;

namespace BackEnd.Interface;

public interface ISelect<T>
{
    IQueryable<T> Select();
}
