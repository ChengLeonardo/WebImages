using System.Numerics;

namespace BackEnd.Models;

public interface IIdSelect<T, N>
{
    T? IdSelect(N id);
}