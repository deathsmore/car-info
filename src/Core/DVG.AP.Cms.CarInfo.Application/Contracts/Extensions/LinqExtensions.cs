using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Extensions; 

public static class LinqExtensions {
     /// <summary>
    /// Left join
    /// </summary>
    /// <param name="outer"></param>
    /// <param name="inner"></param>
    /// <param name="outerKeySelector"></param>
    /// <param name="innerKeySelector"></param>
    /// <param name="resultSelector"></param>
    /// <typeparam name="TOuter"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "TooManyChainedReferences")]
    public static IQueryable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(
        this IQueryable<TOuter> outer, IQueryable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector) {
        const string includeLibraryGroupJoin =
            "System.Linq.IQueryable`1[TResult] GroupJoin[TOuter,TInner,TKey,TResult](System.Linq.IQueryable`1[TOuter], System.Collections.Generic.IEnumerable`1[TInner], System.Linq.Expressions.Expression`1[System.Func`2[TOuter,TKey]], System.Linq.Expressions.Expression`1[System.Func`2[TInner,TKey]], System.Linq.Expressions.Expression`1[System.Func`3[TOuter,System.Collections.Generic.IEnumerable`1[TInner],TResult]])";
        const string includeLibrarySelectMany =
            "System.Linq.IQueryable`1[TResult] SelectMany[TSource,TCollection,TResult](System.Linq.IQueryable`1[TSource], System.Linq.Expressions.Expression`1[System.Func`2[TSource,System.Collections.Generic.IEnumerable`1[TCollection]]], System.Linq.Expressions.Expression`1[System.Func`3[TSource,TCollection,TResult]])";
        var groupJoin = typeof(Queryable).GetMethods().Single(m => m.ToString() == includeLibraryGroupJoin)
            .MakeGenericMethod(typeof(TOuter), typeof(TInner), typeof(TKey),
                typeof(LeftJoinIntermediate<TOuter, TInner>));

        var selectMany = typeof(Queryable).GetMethods().Single(m => m.ToString() == includeLibrarySelectMany)
            .MakeGenericMethod(typeof(LeftJoinIntermediate<TOuter, TInner>), typeof(TInner), typeof(TResult));

        var groupJoinResultSelector =
            (Expression<Func<TOuter, IEnumerable<TInner>, LeftJoinIntermediate<TOuter, TInner>>>)
            ((oneOuter, manyInners) => new LeftJoinIntermediate<TOuter, TInner>
                {OneOuter = oneOuter, ManyInners = manyInners});

        var exprGroupJoin = Expression.Call(groupJoin, outer.Expression, inner.Expression, outerKeySelector,
            innerKeySelector, groupJoinResultSelector);

        var selectManyCollectionSelector = (Expression<Func<LeftJoinIntermediate<TOuter, TInner>, IEnumerable<TInner>>>)
            (t => t.ManyInners.DefaultIfEmpty()!);

        var paramUser = resultSelector.Parameters.First();

        var paramNew = Expression.Parameter(typeof(LeftJoinIntermediate<TOuter, TInner>), "t");
        var propExpr = Expression.Property(paramNew, "OneOuter");

        var selectManyResultSelector = Expression.Lambda(new Replacer(paramUser, propExpr).Visit(resultSelector.Body),
            paramNew, resultSelector.Parameters.Skip(1).First());

        var exprSelectMany = Expression.Call(selectMany, exprGroupJoin, selectManyCollectionSelector,
            selectManyResultSelector);

        return outer.Provider.CreateQuery<TResult>(exprSelectMany);
    }

    private class LeftJoinIntermediate<TOuter, TInner> {
        public TOuter OneOuter { get; set; }
        public IEnumerable<TInner> ManyInners { get; set; }
    }

    private class Replacer : ExpressionVisitor {
        private readonly ParameterExpression _oldParam;
        private readonly Expression _replacement;
        public Replacer(ParameterExpression oldParam, Expression replacement) {
            _oldParam = oldParam;
            _replacement = replacement;
        }

        public override Expression Visit(Expression exp) {
            return exp == _oldParam ? _replacement : base.Visit(exp);
        }
    }   
}