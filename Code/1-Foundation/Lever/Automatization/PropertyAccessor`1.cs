using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Meision.Automatization
{
    public class PropertyAccessor<T>
    {
        private const char DefaultSubItemIndicator = '.';

        #region Field & Property
        private char _subItemIndicator;
        protected char SubItemIndicator
        {
            get
            {
                return this._subItemIndicator;
            }
        }

        private Dictionary<string, Func<T, object>> _getProperieFuncs;
        private Dictionary<string, Action<T, object>> _setProperieFuncs;
        #endregion Field & Property

        #region Constructor
        public PropertyAccessor(char subItemIndicator = DefaultSubItemIndicator)
        {
            this._subItemIndicator = subItemIndicator;

            this._getProperieFuncs = new Dictionary<string, Func<T, object>>();
            this._setProperieFuncs = new Dictionary<string, Action<T, object>>();
        }
        #endregion Constructor

        #region Method
        public object GetValue(T item, string propertyName)
        {
            ThrowHelper.ArgumentNull((item == null), nameof(item));
            ThrowHelper.ArgumentNull((propertyName == null), nameof(propertyName));

            if (!this._getProperieFuncs.ContainsKey(propertyName))
            {
                this._getProperieFuncs[propertyName] = this.CreateGetPropertyFunc(propertyName);
            }

            return this._getProperieFuncs[propertyName](item);
        }
        protected virtual Func<T, object> CreateGetPropertyFunc(string propertyName)
        {
            Expression expression;

            ParameterExpression p = Expression.Parameter(typeof(T), "p");

            string[] segments = propertyName.Split(this._subItemIndicator);
            if (segments.Length > 1)
            {
                Expression test = Expression.Equal(Expression.Property(p, segments[0]), Expression.Constant(null));
                for (int i = 1; i < segments.Length - 1; i++)
                {
                    Expression single = p;
                    for (int j = 0; j <= i; j++)
                    {
                        single = Expression.Property(single, segments[j]);
                    }
                    test = Expression.OrElse(test, Expression.Equal(single, Expression.Constant(null)));
                }

                Expression ifFalse = p;
                for (int i = 0; i < segments.Length; i++)
                {
                    ifFalse = Expression.Property(ifFalse, segments[i]);
                }
                ifFalse = Expression.Convert(ifFalse, typeof(object));

                expression = Expression.Condition(test, Expression.Constant(null), ifFalse);

            }
            else
            {
                expression = p;
                expression = Expression.Property(expression, segments[0]);
                expression = Expression.Convert(expression, typeof(object));
            }

            LambdaExpression lambda = Expression.Lambda(expression, new ParameterExpression[] { p });
            Func<T, object> func = lambda.Compile() as Func<T, object>;
            return func;
        }

        public void SetValue(T item, string propertyName, object value)
        {
            ThrowHelper.ArgumentNull((item == null), nameof(item));
            ThrowHelper.ArgumentNull((propertyName == null), nameof(propertyName));

            if (!this._setProperieFuncs.ContainsKey(propertyName))
            {
                this._setProperieFuncs[propertyName] = this.CreateSetPropertyAction(propertyName);
            }

            this._setProperieFuncs[propertyName](item, value);
        }

        protected virtual Action<T, object> CreateSetPropertyAction(string propertyName)
        {
            Expression expression;

            ParameterExpression p = Expression.Parameter(typeof(T), "p");
            ParameterExpression v = Expression.Parameter(typeof(object), "v");

            string[] segments = propertyName.Split(this._subItemIndicator);
            if (segments.Length > 1)
            {
                Expression test = Expression.Equal(Expression.Property(p, segments[0]), Expression.Constant(null));
                for (int i = 1; i < segments.Length - 1; i++)
                {
                    Expression single = p;
                    for (int j = 0; j <= i; j++)
                    {
                        single = Expression.Property(single, segments[j]);
                    }
                    test = Expression.OrElse(test, Expression.Equal(single, Expression.Constant(null)));
                }

                Expression ifFalse = p;
                Type type = typeof(T);
                for (int i = 0; i < segments.Length; i++)
                {
                    ifFalse = Expression.Property(ifFalse, segments[i]);
                    type = type.GetProperty(segments[i]).PropertyType;
                }
                ifFalse = Expression.Assign(ifFalse, Expression.Convert(v, type));

                expression = Expression.IfThenElse(test, Expression.Empty(), ifFalse);
            }
            else
            {
                Type type = typeof(T);

                expression = p;
                expression = Expression.Property(expression, segments[0]);
                type = type.GetProperty(segments[0]).PropertyType;
                expression = Expression.Assign(expression, Expression.Convert(v, type));
            }

            LambdaExpression lambda = Expression.Lambda<Action<T, object>>(expression, new ParameterExpression[] { p, v });
            Action<T, object> action = lambda.Compile() as Action<T, object>;
            return action;
        }
        #endregion Method
    }
}
