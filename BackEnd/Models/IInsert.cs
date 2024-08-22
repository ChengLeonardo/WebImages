namespace BackEnd.Models;

public interface IInsert<T, N>
{
    N Insert(T objeto, string idAutoIncrement);
}