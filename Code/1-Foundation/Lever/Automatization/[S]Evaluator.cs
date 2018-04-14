using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Meision.Resources;

namespace Meision.Automatization
{
    public static class Evaluator
    {
        /// <summary>
        /// Compute expression, throw exception if error
        /// </summary>
        /// <param name="expression">expression</param>
        /// <returns>result</returns>
        //public static T Evaluate<T>(string expression)
        //{
        //    LambdaExpression lambda = DynamicExpression.ParseLambda(new ParameterExpression[0], typeof(T), expression);
        //    return (T)lambda.Compile().DynamicInvoke();
        //}

        //public static object EvaluateWebService(string url, string methodName, object[] arguments)
        //{
        //    // Get ServiceDescriptionImporter
        //    ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
        //    using (WebClient client = new WebClient())
        //    using (Stream stream = client.OpenRead(url + "?wsdl"))
        //    {
        //        ServiceDescription sd = ServiceDescription.Read(stream);
        //        sdi.AddServiceDescription(sd, null, null);
        //    }
        
        //    // Generate assembly
        //    CodeNamespace codeNamespace = new CodeNamespace();
        //    CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
        //    codeCompileUnit.Namespaces.Add(codeNamespace);
        //    sdi.Import(codeNamespace, codeCompileUnit);

        //    CodeDomProvider provider = new CSharpCodeProvider();
        //    CompilerParameters compilerParameteres = new CompilerParameters();
        //    compilerParameteres.GenerateExecutable = false;
        //    compilerParameteres.GenerateInMemory = true;
        //    compilerParameteres.ReferencedAssemblies.Add("System.dll");

        //    CompilerResults cr = provider.CompileAssemblyFromDom(compilerParameteres, codeCompileUnit);
        //    if (cr.Errors.HasErrors)
        //    {
        //        StringBuilder builder = new System.Text.StringBuilder();
        //        foreach (CompilerError error in cr.Errors)
        //        {
        //            builder.AppendLine(error.ToString());
        //        }
        //        throw new InvalidOperationException(builder.ToString());
        //    }

        //    Assembly assembly = cr.CompiledAssembly;
        //    Type type = assembly.GetType(sdi.ServiceDescriptions[0].Services[0].Name, true, true);
        //    object instance = Activator.CreateInstance(type);
        //    MethodInfo method = type.GetMethod(methodName);
        //    object value = method.Invoke(instance, arguments);
        //    return value;
        //}


        //public static object Calculate(string formula, params object[] values)
        //{
        //    string input = formula.Trim();
        //    string regex = @"[^\+\-\*\/\%\(\)]+";

        //    // Replace formula with actual values.
        //    int index = 0;
        //    string expression = Regex.Replace(
        //        input,
        //        regex,
        //        delegate(Match match)
        //        {
        //            // pure numeric.
        //            if (Regex.IsMatch(match.Value, @"^[\d\ \.]+$"))
        //            {
        //                return match.Value;
        //            }

        //            return values[index++].ToString();
        //        });


        //    return Eval(expression);
        //}

        public static string GetMethodName<T>(Expression<Action<T>> expression)
        {
            MethodCallExpression body = expression.Body as System.Linq.Expressions.MethodCallExpression;
            return body.Method.Name;
        }

        public static string GetMethodName<T>(Expression<Func<T, Delegate>> expression)
        {
            MethodCallExpression body = expression.Body as System.Linq.Expressions.MethodCallExpression;
            return body.Method.Name;
        }

        /// <summary>
        /// Get property name from call expression
        /// </summary>
        /// <typeparam name="TType">caller class</typeparam>
        /// <param name="expression">calling expression</param>
        /// <param name="indicator">property indicator</param>
        /// <returns>property expression</returns>
        public static string GetPropertyName<TType>(Expression<Func<TType, object>> expression, string indicator)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                UnaryExpression unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    body = unaryExpression.Operand as MemberExpression;
                }
            }

            Stack<string> segments = new Stack<string>();
            while (body != null)
            {
                segments.Push(body.Member.Name);
                body = body.Expression as MemberExpression;
            }
            if (segments.Count == 0)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder(segments.Pop());
            while (segments.Count > 0)
            {
                builder.Append(indicator);
                builder.Append(segments.Pop());
            }
            return builder.ToString();
        }

        public static string GetPropertyName<TType>(Expression<Func<TType, object>> expression)
        {
            return GetPropertyName(expression, Meision.Text.StringConstant.Sub.ToString());
        }

        public static T ExecuteFunc<T>(Func<T> func)
        {
            return func();
        }

        public static Func<TInput, TOutput> CreateRangeFunc<TInput, TOutput>(string[] ranges, object defaultValue = null)
            where TInput : IComparable
            where TOutput : IComparable
        {
            ThrowHelper.ArgumentNull((ranges == null), nameof(ranges));

            if (defaultValue != null)
            {
                if (defaultValue.GetType() != typeof(TOutput))
                {
                    throw new ArgumentException(SR.Evaluator_Exception_ExpectedType.FormatWith(typeof(TOutput).ToString()));
                }
            }

            Expression expression;
            if (defaultValue != null)
            {
                expression = Expression.Constant(defaultValue, typeof(TOutput));
            }
            else
            {
                expression = Expression.Throw(Expression.New(typeof(ArgumentOutOfRangeException)), typeof(TOutput));
            }
            ParameterExpression p = Expression.Parameter(typeof(TInput), "p");
            if (ranges.Length != 0)
            {
                Regex regex = new Regex(@"^(?<lowerComparison>\(|\[)(?<lower>.*?)\,(?<upper>.*?)(?<upperComparison>\)|\])\=(?<result>.+)$", RegexOptions.Compiled);
                Tuple<Expression, Expression>[] expressionSegments = new Tuple<Expression, Expression>[ranges.Length];
                for (int i = 0; i < ranges.Length; i++)
                {
                    string range = ranges[i];
                    if (range == null)
                    {
                        throw new ArgumentException(SR.Evaluator_Exception_NullRangeExpression.FormatWith(i));
                    }
                    Type[] inputTypes = new Type[] { typeof(int), typeof(double), typeof(decimal) };
                    if (!inputTypes.Contains(typeof(TInput)))
                    {
                        string text = string.Join(",", inputTypes.MapArray(item => item.ToString()));
                        throw new ArgumentException(SR.Evaluator_Exception_InvalidTInput.FormatWith(text));
                    }
                    Type[] outputTypes = new Type[] { typeof(int), typeof(double), typeof(char), typeof(string), typeof(decimal) };
                    if (!outputTypes.Contains(typeof(TOutput)))
                    {
                        string text = string.Join(",", outputTypes.MapArray(item => item.ToString()));
                        throw new ArgumentException(SR.Evaluator_Exception_InvalidTOutput.FormatWith(text));
                    }

                    Match match = regex.Match(range);
                    if (!match.Success)
                    {
                        throw new ArgumentException(SR.Evaluator_Exception_InvalidRangeExpression.FormatWith(range, i));
                    }
                    string lowerComparison = match.Groups["lowerComparison"].Value;
                    string lower = match.Groups["lower"].Value;
                    string upper = match.Groups["upper"].Value;
                    string upperComparison = match.Groups["upperComparison"].Value;
                    string result = match.Groups["result"].Value;
                    if (string.IsNullOrEmpty(lower) && string.IsNullOrEmpty(upper))
                    {
                        throw new ArgumentException(SR.Evaluator_Exception_InvalidRangeExpression.FormatWith(range));
                    }

                    MethodInfo inputConvert = typeof(Convert).GetMethod("To" + typeof(TInput).Name, new Type[] { typeof(string) });
                    MethodInfo outputConvert = typeof(Convert).GetMethod("To" + typeof(TOutput).Name, new Type[] { typeof(string) });

                    // Left expression
                    Expression leftExpression = null;
                    if (!string.IsNullOrEmpty(lower))
                    {
                        object leftValue = inputConvert.Invoke(null, new object[] { lower });
                        if (lowerComparison == "[")
                        {
                            leftExpression = Expression.LessThanOrEqual(Expression.Constant(leftValue, typeof(TInput)), p);
                        }
                        else
                        {
                            leftExpression = Expression.LessThan(Expression.Constant(leftValue, typeof(TInput)), p);
                        }
                    }
                    // Right expression
                    Expression rightExpression = null;
                    if (!string.IsNullOrEmpty(upper))
                    {
                        object rightValue = inputConvert.Invoke(null, new object[] { upper });
                        if (upperComparison == "]")
                        {
                            rightExpression = Expression.LessThanOrEqual(p, Expression.Constant(rightValue, typeof(TInput)));
                        }
                        else
                        {
                            rightExpression = Expression.LessThan(p, Expression.Constant(rightValue, typeof(TInput)));
                        }
                    }
                    // Result expression
                    object resultValue = outputConvert.Invoke(null, new object[] { result });
                    Expression resultExpression = Expression.Constant(resultValue);

                    if ((leftExpression != null) && (rightExpression != null))
                    {
                        expressionSegments[i] = new Tuple<Expression, Expression>(Expression.And(leftExpression, rightExpression), resultExpression);
                    }
                    else
                    {
                        expressionSegments[i] = new Tuple<Expression, Expression>(leftExpression ?? rightExpression, resultExpression);
                    }
                }

                // Last one
                for (int i = expressionSegments.Length - 1; i >= 0; i--)
                {
                    expression = Expression.Condition(
                        expressionSegments[i].Item1,
                        expressionSegments[i].Item2,
                        expression,
                        typeof(TOutput));
                }
            }

            LambdaExpression lambda = Expression.Lambda(expression, new ParameterExpression[] { p });
            Func<TInput, TOutput> func = lambda.Compile() as Func<TInput, TOutput>;
            return func;
        }


    }
}