using System.Numerics;

namespace BackEnd.Interface;

public interface IIdSelect<T, N>
{
    T? IdSelect(N id);
}