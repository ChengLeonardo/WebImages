using System.Numerics;

namespace BackEnd.Interface;

public interface ISelect<T>
{
    List<T> Select();
}