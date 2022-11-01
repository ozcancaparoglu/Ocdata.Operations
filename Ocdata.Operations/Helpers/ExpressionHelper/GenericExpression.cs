using Ocdata.Operations.Enums;
using System.Linq.Expressions;

namespace Ocdata.Operations.Helpers.ExpressionHelper
{
    public static class GenericExpression
    {
        /// <summary>
        /// Creates generic expression with FilterModel objects.
        /// </summary>
        /// <typeparam name="T">Type of expression</typeparam>
        /// <param name="filterModel">Filters</param>
        /// <param name="joint">And Or joint</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Binding<T>(FilterModel filterModel, ExpressionJoint joint = ExpressionJoint.And) where T : class
        {
            Expression<Func<T, bool>> finalExpression = null;

            foreach (var filter in filterModel.Filters)
            {
                var value = filter.Value;

                Expression<Func<T, bool>> predicate = CreatePredicate<T>(filter.Field, value, filter.Operator);

                if (finalExpression == null)
                    finalExpression = predicate;
                else if (joint == ExpressionJoint.And)
                    finalExpression = ExpressionExtensions.And(finalExpression, predicate);
                else
                    finalExpression = ExpressionExtensions.Or(finalExpression, predicate);
            }
            return finalExpression;
        }

        private static Expression<Func<T, bool>> CreatePredicate<T>(string field, object searchValue, FilterOperatorEnum filterOperator) where T : class
        {
            var xType = typeof(T);
            var x = Expression.Parameter(xType, "type");
            var column = xType.GetProperties().FirstOrDefault(p => p.Name.ToLowerInvariant() == field.ToLowerInvariant());

            Expression body = null;

            switch (filterOperator)
            {
                case FilterOperatorEnum.IsEqualTo:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Equal(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(searchValue));
                    break;
                case FilterOperatorEnum.IsNotEqualTo:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.NotEqual(
                     Expression.PropertyOrField(x, field),
                     Expression.Constant(searchValue));
                    break;
                case FilterOperatorEnum.StartsWith:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Call(Expression.Property(x, column.Name), typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(searchValue, typeof(string)));
                    break;
                case FilterOperatorEnum.Contains:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Call(Expression.Property(x, column.Name), typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(searchValue, typeof(string)));
                    break;
                case FilterOperatorEnum.DoesNotContain:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Not(Expression.Constant(searchValue, typeof(string)), typeof(string).GetMethod("Contains", new[] { typeof(string) }));
                    break;
                case FilterOperatorEnum.EndsWith:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Call(Expression.Property(x, column.Name), typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(searchValue, typeof(string)));
                    break;
                case FilterOperatorEnum.IsNull:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Equal(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.IsNotNull:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.NotEqual(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.IsEmpty:
                    body = column == null
                 ? (Expression)Expression.Constant(true)
                 : Expression.Equal(
                     Expression.PropertyOrField(x, field),
                     Expression.Constant(string.Empty, typeof(object)));
                    break;
                case FilterOperatorEnum.IsNotEmpty:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.NotEqual(
                  Expression.PropertyOrField(x, field),
                  Expression.Constant(string.Empty, typeof(object)));
                    break;
                case FilterOperatorEnum.HasNoValue:
                    body = column == null
               ? (Expression)Expression.Constant(true)
               : Expression.Equal(
                   Expression.PropertyOrField(x, field),
                   Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.HasValue:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.Equal(
                  Expression.PropertyOrField(x, field),
                  Expression.Constant(null, typeof(object)));
                    break;
                case FilterOperatorEnum.Between:
                    body = column == null
               ? (Expression)Expression.Constant(true)
                : Expression.And(Expression.LessThanOrEqual(Expression.PropertyOrField(x, field),
                Expression.Constant(Convert.ToDecimal(searchValue.ToString().Split(",")[1]), typeof(decimal))),
                Expression.GreaterThanOrEqual(Expression.PropertyOrField(x, field),
                Expression.Constant(Convert.ToDecimal(searchValue.ToString().Split(",")[0]), typeof(decimal))));
                    break;
                case FilterOperatorEnum.IsEqualToNotNullGuid:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.Equal(Expression.PropertyOrField(x, field),
              Expression.Constant(new Guid(searchValue.ToString()), typeof(Guid)));
                    break;
                case FilterOperatorEnum.IsEqualToGuid:
                    body = column == null
              ? (Expression)Expression.Constant(true)
              : Expression.Equal(Expression.PropertyOrField(x, field),
              Expression.Constant(new Guid(searchValue.ToString()), typeof(Guid?)));
                    break;
                case FilterOperatorEnum.GreaterThan:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.GreaterThan(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(searchValue));
                    break;
                case FilterOperatorEnum.GreaterThanOrEqual:
                    body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.GreaterThanOrEqual(
                    Expression.PropertyOrField(x, field),
                    Expression.Constant(searchValue));
                    break;
                default:
                    break;
            }

            return Expression.Lambda<Func<T, bool>>(body, x);
        }
    }
}
