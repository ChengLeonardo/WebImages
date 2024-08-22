using System.Numerics;

namespace BackEnd.Models;

public interface ISelect<T>
{
    List<T> Select();
}