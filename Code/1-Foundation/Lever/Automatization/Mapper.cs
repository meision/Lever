using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Meision.Automatization
{
    public static class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            TDestination destination = new TDestination();
            Mapper<TSource, TDestination>.Map(source, destination);
            return destination;
        }

        public static void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            Mapper<TSource, TDestination>.Map(source, destination);
        }
    }

    internal static class Mapper<TSource, TDestination>
    {
        private static readonly Action<TSource, TDestination> __mapping = Evaluator.ExecuteFunc<Action<TSource, TDestination>>(() =>
        {
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);

            List<Expression> expressions = new List<Expression>();

            ParameterExpression sourceExpression = Expression.Parameter(sourceType, "source");
            ParameterExpression destinationExpression = Expression.Parameter(destinationType, "destination");

            BlockExpression blockExpression;
            if ((typeof(DataRow).IsAssignableFrom(typeof(TSource)))
             || (typeof(DbDataReader).IsAssignableFrom(typeof(TSource))))
            {
                bool isDataRow = typeof(DataRow).IsAssignableFrom(typeof(TSource));

                // [DataRow] DataTable table = source.Table;
                ParameterExpression tableExpression = null;
                if (isDataRow)
                {
                    tableExpression = Expression.Parameter(typeof(DataTable), "table");
                    expressions.Add(Expression.Assign(tableExpression, Expression.Property(sourceExpression, "Table")));
                }
                // [DataRow] int count = table.Columns.Count;
                // [DbDataReader] int count = source.FieldCount;
                ParameterExpression countExpression = Expression.Parameter(typeof(int), "count");
                if (isDataRow)
                {
                    expressions.Add(Expression.Assign(countExpression, Expression.Property(Expression.Property(tableExpression, nameof(DataTable.Columns)), nameof(DataColumnCollection.Count))));
                }
                else
                {
                    expressions.Add(Expression.Assign(countExpression, Expression.Property(sourceExpression, nameof(DbDataReader.FieldCount))));
                }
                // int i = 0;
                ParameterExpression iExpression = Expression.Parameter(typeof(int), "i");
                expressions.Add(Expression.Assign(iExpression, Expression.Constant(0)));
                // if (i < count)
                // {
                //     <block>
                //     i++;
                // }
                // else
                // {
                //     break;
                // }
                ParameterExpression oExpression = Expression.Parameter(typeof(object), "o");
                LabelTarget label = Expression.Label();
                expressions.Add(Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(iExpression, countExpression),
                        Expression.Block(
                            new ParameterExpression[] { oExpression },
                            // o = source[i];
                            Expression.Assign(oExpression, Expression.Property(sourceExpression, "Item", iExpression)),
                            // [DataRow] switch (table.Columns[i].Caption)
                            // [DbDataReader] switch (source.GetName(i))
                            // {
                            //     case "XXX":
                            //         [DataRow] if (!Convert.IsDBNull(o)) destination.XXX = (Type)Convert.Change(o, Type);
                            //         [DbDataReader] if (!Convert.IsDBNull(o)) destination.XXX = (Type)o;
                            //         break;
                            // }
                            Expression.Switch(
                                typeof(void), // type
                                isDataRow ? // switchValue
                                    (Expression)Expression.Property(Expression.Property(Expression.Property(tableExpression, nameof(DataTable.Columns)), "Item", iExpression), nameof(DataColumn.Caption))
                                    : (Expression)Expression.Call(sourceExpression, typeof(DbDataReader).GetMethod(nameof(DbDataReader.GetName), BindingFlags.Public | BindingFlags.Instance), iExpression),
                                null, // defaultBody
                                null, // comparision
                                destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && (p.GetIndexParameters().Length == 0)).Select
                                (
                                    p => Expression.SwitchCase(
                                        Expression.IfThen(
                                            Expression.Not(Expression.Call(typeof(Convert).GetMethod(nameof(Convert.IsDBNull), BindingFlags.Public | BindingFlags.Static), oExpression)),
                                            isDataRow ?
                                                (Expression)Expression.Assign(Expression.Property(destinationExpression, p), Expression.Convert(Expression.Call(null, typeof(Convert).GetMethod(nameof(Convert.ChangeType), new Type[] { typeof(object), typeof(Type) }), oExpression, Expression.Constant(p.PropertyType)), p.PropertyType))
                                                : (Expression)Expression.Assign(Expression.Property(destinationExpression, p), Expression.Convert(oExpression, p.PropertyType))),
                                        Expression.Constant(p.Name))
                                ).ToArray()), // cases
                            Expression.Assign(iExpression, Expression.Increment(iExpression))), // i = i++;
                        Expression.Break(label)),
                    label));

                blockExpression = BlockExpression.Block(
                    isDataRow ? new ParameterExpression[] { tableExpression, countExpression, iExpression } : new ParameterExpression[] { countExpression, iExpression },
                    expressions);
            }
            else
            {
                // destination.XXX = source.XXX;
                foreach (PropertyInfo destinationProperty in destinationType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite && (p.GetIndexParameters().Length == 0)))
                {
                    PropertyInfo sourceProperty = sourceType.GetProperty(destinationProperty.Name, BindingFlags.Public | BindingFlags.Instance);
                    if ((sourceProperty == null) || (!sourceProperty.CanRead) || (sourceProperty.GetIndexParameters().Length > 0))
                    {
                        continue;
                    }

                    Expression assignExpression = Expression.Assign(
                        Expression.Property(destinationExpression, destinationProperty),
                        Expression.Property(sourceExpression, sourceProperty));
                    expressions.Add(assignExpression);
                }

                blockExpression = BlockExpression.Block(expressions);
            }

            Action<TSource, TDestination> func = Expression.Lambda<Action<TSource, TDestination>>(blockExpression, sourceExpression, destinationExpression).Compile() as Action<TSource, TDestination>;
            return func;
        });

        public static void Map(TSource source, TDestination destination)
        {
            __mapping(source, destination);
        }

    }
}
