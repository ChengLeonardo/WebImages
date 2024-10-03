using System.Numerics;

namespace BackEnd.Interface;

public interface IRepoBase<T, N> : ISelect<T>, IIdSelect<T, N>, IUpdate<T>, IDelete<T>, IInsert<T, N>, ISelectWhere<T>
{
}