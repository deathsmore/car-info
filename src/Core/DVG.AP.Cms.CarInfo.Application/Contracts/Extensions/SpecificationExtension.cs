using System.Linq.Expressions;
using DVG.AutoPortal.Specification;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Extensions;

public abstract class DvgSpecification<T> : Specification<T>
{
    public static readonly DvgSpecification<T> All = new IdentitySpecification<T>();

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public abstract Expression<Func<T, bool>> ToExpression();

    public DvgSpecification<T> And(DvgSpecification<T> specification)
    {
        if (this == All)
            return specification;
        if (specification == All)
            return this;

        return new AndSpecification<T>(this, specification);
    }

    public DvgSpecification<T> Or(DvgSpecification<T> specification)
    {
        if (this == All || specification == All)
            return All;

        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}

internal sealed class IdentitySpecification<T> : DvgSpecification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}

internal sealed class AndSpecification<T> : DvgSpecification<T>
{
    private readonly DvgSpecification<T> _left;
    private readonly DvgSpecification<T> _right;

    public AndSpecification(DvgSpecification<T> left, DvgSpecification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
    }
}
internal sealed class OrSpecification<T> : DvgSpecification<T>
{
    private readonly DvgSpecification<T> _left;
    private readonly DvgSpecification<T> _right;

    public OrSpecification(DvgSpecification<T> left, DvgSpecification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
    }
}


internal sealed class NotSpecification<T> : DvgSpecification<T>
{
    private readonly DvgSpecification<T> _specification;

    public NotSpecification(DvgSpecification<T> specification)
    {
        _specification = specification;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var expression = _specification.ToExpression();
        var notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
    }
}
