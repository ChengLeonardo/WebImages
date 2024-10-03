namespace BackEnd.Interface;

public interface IInsert<T, N>
{
    N Insert(T objeto, string tipoIdAutoIncrement);
}