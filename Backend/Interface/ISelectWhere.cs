using System.Linq.Expressions;

namespace BackEnd.Interface;

public interface ISelectWhere<T>
{
    public List<T> SelectWhere(Expression<Func<T, bool>> predicate);
}
