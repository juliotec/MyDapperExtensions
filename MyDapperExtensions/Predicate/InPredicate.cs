using DapperExtensions.Sql;
using System.Collections;
using System.Collections.Generic;

namespace DapperExtensions.Predicate
{
    public interface IInPredicate : IBasePredicate
    {
        ICollection Collection { get; }
        bool Not { get; set; }
    }

    public class InPredicate<T> : BasePredicate, IInPredicate
    {
        public ICollection Collection { get; }
        public bool Not { get; set; }

        public InPredicate(ICollection collection, string propertyName, bool isNot = false)
        {
            PropertyName = propertyName;
            Collection = collection;
            Not = isNot;
        }

        public override string GetSql(ISqlGenerator sqlGenerator, IDictionary<string, object> parameters, bool isDml = false)
        {
            var columnName = GetColumnName(typeof(T), sqlGenerator, PropertyName);
            var @params = new List<string>();

            foreach (var item in Collection)
            {
                @params.Add(parameters.SetParameterName(ReflectionHelper.GetParameter(typeof(T), sqlGenerator, PropertyName, item), sqlGenerator.Configuration.Dialect.ParameterPrefix));
            }

            return $"({columnName.Trim()} {GetIsNotStatement(Not)}IN ({string.Join(", ", @params)}))";
        }

        private static string GetIsNotStatement(bool not)
        {
            return not ? "NOT " : string.Empty;
        }
    }
}
