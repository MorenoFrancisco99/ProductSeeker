using System.Linq.Expressions;
using ProductSeeker.Data.Models;

public interface IHasSpecPredicate<TSpec> where TSpec : ProductSpecModel
{
    Expression<Func<TSpec, bool>> MatchPredicate { get; }
    TSpec ToEntity(int coreId);
}