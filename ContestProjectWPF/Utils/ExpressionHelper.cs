using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Expr = System.Linq.Expressions;

//------------------------------------------------------------------------
//	RZL KIS                                                    (c)2012 RZL
//
//
//	MS-Windows										    (w) Thomas Humer
//------------------------------------------------------------------------
//  ExpressionHelper.cs
//------------------------------------------------------------------------
namespace ContestProject.Utils
{
    /// <summary>
    /// Hilfsmethoden für Expressions.
    /// </summary>
    public static class ExpressionHelper
    {
        #region Public Methoden

        /// <summary>
        /// Extrahiert den Propertynamen der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befinden soll.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <param name="allowEmptyExpression">Erlaubt leere Property Expressions (z.B. 'o => o') --> Liefert einen Leerstring</param>
        /// <returns>Name der Property.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static string ExtractPropertyName<T>(Expression<Func<T, object>> propertyExpression, bool allowEmptyExpression = false)
        {
            return BuildPropertyPath(ExtractPropertyMemberInfo(propertyExpression, allowEmptyExpression));
        }

        /// <summary>
        /// Extrahiert den Propertynamen der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befinden soll.</typeparam>
        /// <typeparam name="P">Rückgabetyp des Properties.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <param name="allowEmptyExpression">Erlaubt leere Property Expressions (z.B. 'o => o') --> Liefert einen Leerstring</param>
        /// <returns>Name der Property.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static string ExtractPropertyName<T, P>(Expression<Func<T, P>> propertyExpression, bool allowEmptyExpression = false)
        {
            return BuildPropertyPath(ExtractPropertyMemberInfo(propertyExpression, allowEmptyExpression));
        }

        /// <summary>
        /// Extrahiert den Propertynamen der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befinden soll.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <param name="allowEmptyExpression">Erlaubt leere Property Expressions (z.B. 'o => o') --> Liefert einen Leerstring</param>
        /// <returns>Name der Property.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression, bool allowEmptyExpression = false)
        {
            return BuildPropertyPath(ExtractPropertyMemberInfo(propertyExpression, allowEmptyExpression));
        }

        /// <summary>
        /// Extrahiert den Propertynamen der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befinden soll.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Name der Property.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static string ExtractPropertyName(LambdaExpression propertyExpression)
        {
            return BuildPropertyPath(ExtractPropertyMemberInfo(propertyExpression));
        }

        /// <summary>
        /// Extrahiert den Propertynamen der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befinden soll.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Name der Property.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static string ExtractTopLevelPropertyName(LambdaExpression propertyExpression)
        {
            IMemberInfoExpression exprInfo = ExtractPropertyMemberInfo(propertyExpression).LastOrDefault();

            if (exprInfo != null && exprInfo is PropertyInfoExpression)
                return ((PropertyInfoExpression)exprInfo).PropertyInfo.Name;

            return null;
        }

        /// <summary>
        /// Extrahiert den Typ des Properties.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <typeparam name="P">Rückgabetyp des Properties.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Den Typ des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static Type ExtractPropertyType<T, P>(Expression<Func<T, P>> propertyExpression)
        {
            return ExtractPropertyMemberInfo(propertyExpression).LastOrDefault().ReturnType;
        }

        /// <summary>
        /// Extrahiert den Typ des Properties.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Den Typ des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static Type ExtractPropertyType<T>(Expression<Func<T, object>> propertyExpression)
        {
            return ExtractPropertyMemberInfo(propertyExpression).LastOrDefault().ReturnType;
        }

        /// <summary>
        /// Extrahiert den Typ des Properties.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Den Typ des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static Type ExtractPropertyType<T>(Expression<Func<T>> propertyExpression)
        {
            return ExtractPropertyMemberInfo(propertyExpression).LastOrDefault().ReturnType;
        }

        /// <summary>
        /// Extrahiert den Typ des Properties.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Den Typ des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static Type ExtractPropertyType(LambdaExpression propertyExpression)
        {
            return ExtractPropertyMemberInfo(propertyExpression).LastOrDefault().ReturnType;
        }

        /// <summary>
        /// Extrahiert ein PropertyInfo aus der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Das PropertyInfo des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static PropertyInfo ExtractPropertyInfo<T>(Expression<Func<T, object>> propertyExpression)
        {
            return ExtractPropertyInfo<T, object>(propertyExpression);
        }

        /// <summary>
        /// Extrahiert ein PropertyInfo aus der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Das PropertyInfo des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static PropertyInfo ExtractPropertyInfo<T, P>(Expression<Func<T, P>> propertyExpression)
        {
            IMemberInfoExpression exprInfo = ExtractPropertyMemberInfo(propertyExpression).LastOrDefault();

            if (exprInfo != null && exprInfo is PropertyInfoExpression)
                return ((PropertyInfoExpression)exprInfo).PropertyInfo;

            return null;
        }

        /// <summary>
        /// Extrahiert ein PropertyInfo aus der Expression.
        /// </summary>
        /// <typeparam name="T">Typ des Objekts, in der sich das Property befindet.</typeparam>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>Das PropertyInfo des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        public static PropertyInfo ExtractPropertyInfo(LambdaExpression propertyExpression)
        {
            IMemberInfoExpression exprInfo = ExtractPropertyMemberInfo(propertyExpression).LastOrDefault();

            if (exprInfo != null && exprInfo is PropertyInfoExpression)
                return ((PropertyInfoExpression)exprInfo).PropertyInfo;

            return null;
        }

        public static Func<T, TResult> GetGetter<T, TResult>(LambdaExpression expression)
        {
            Func<T, TResult> getter = ExpressionHelper.BuildSafeAccessor<T, TResult>(Expr.Expression.Lambda<Func<T, TResult>>(
                                        Expr.Expression.Convert(expression.Body, typeof(TResult)), expression.Parameters));

            return getter;
        }

        public static Action<T1, T2> GetSetter<T1, T2>(LambdaExpression expression)
        {
            ParameterExpression valueParamExpr = Expr.Expression.Parameter(typeof(T2));
            Expr.Expression targetExpr = expression.Body is UnaryExpression ? ((UnaryExpression)expression.Body).Operand : expression.Body;

            Action<T1, T2> setter = Expr.Expression.Lambda<Action<T1, T2>>(
                Expr.Expression.Assign(targetExpr, Expr.Expression.Convert(valueParamExpr, targetExpr.Type)),
                expression.Parameters.Single(),
                valueParamExpr).Compile();

            return setter;
        }

        #endregion

        #region NullSafe Expression

        public static Func<TO, TP> BuildSafeAccessor<TO, TP>(Expression<Func<TO, TP>> propertyExpression)
        {
            Expr.Expression curExpression = propertyExpression.Body;
            while (curExpression is UnaryExpression)
                curExpression = ((UnaryExpression)curExpression).Operand;

            var properties = GetProperties(curExpression);
            var parameter = propertyExpression.Parameters.Single();
            var nullExpression = Expr.Expression.Constant(default(TP), typeof(TP));

            var lambdaBody = BuildSafeAccessorExpression(parameter, properties, nullExpression);
            var lambda = Expr.Expression.Lambda<Func<TO, TP>>(lambdaBody, parameter);

            return lambda.Compile();
        }

        private static Expr.Expression BuildSafeAccessorExpression(Expr.Expression init, IEnumerable<PropertyInfo> properties, Expr.Expression nullExpression)
        {
            if (!properties.Any())
                return init;

            var propertyAccess = Expr.Expression.Property(init, properties.First());
            var nextStep = Expr.Expression.Convert(BuildSafeAccessorExpression(propertyAccess, properties.Skip(1), nullExpression), nullExpression.Type);

            return Expr.Expression.Condition(
                Expr.Expression.ReferenceEqual(init, Expr.Expression.Constant(null)), nullExpression, nextStep);
        }

        private static IEnumerable<PropertyInfo> GetProperties(Expr.Expression expression)
        {
            var results = new List<PropertyInfo>();

            while (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;
                results.Add((PropertyInfo)memberExpression.Member);
                expression = memberExpression.Expression;
            }

            while (expression is UnaryExpression)
                expression = ((UnaryExpression)expression).Operand;

            if (!(expression is ParameterExpression))
                throw new ArgumentException();

            results.Reverse();

            return results;
        }

        #endregion

        #region Private Hilfsmethoden

        /// <summary>
        /// Extrahiert das PropertyInfo anhand einer Property-Expression.
        /// </summary>
        /// <param name="propertyExpression">Die Property Expression (z.B. 'o => o.PropertyName')</param>
        /// <returns>PropertyInfo des Properties.</returns>
        /// <exception cref="ArgumentNullException">Wenn <paramref name="propertyExpression"/> null ist.</exception>
        /// <exception cref="ArgumentException">Wird geworfen wenn:<br/>
        ///     Ist keine <see cref="MemberExpression"/><br/>
        ///     Die <see cref="MemberExpression"/> ist kein Property.<br/>
        ///     Oder, das Property ist statisch.
        /// </exception>
        private static IEnumerable<IMemberInfoExpression> ExtractPropertyMemberInfo(LambdaExpression propertyExpression, bool allowEmpty = false)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");

            System.Linq.Expressions.Expression curExpression = propertyExpression.Body;

            IList<IMemberInfoExpression> memberInfoExpressionList = new List<IMemberInfoExpression>();
            do
            {
                if (curExpression is UnaryExpression)
                    curExpression = ((UnaryExpression)curExpression).Operand;
                else if (curExpression is MemberExpression)
                {
                    MemberExpression memberExpression = (MemberExpression)curExpression;
                    if (memberExpression.Member is PropertyInfo)
                    {
                        var property = (PropertyInfo)memberExpression.Member;
                        if (property == null)
                            throw new ArgumentException("Die Expression enthält kein Property-Expression.", "propertyExpression");

                        MethodInfo getMethod = property.GetGetMethod(true);
                        if (getMethod.IsStatic)
                            throw new ArgumentException("Das Property in der Expression darf nicht statisch sein.", "propertyExpression");

                        memberInfoExpressionList.Insert(0, new PropertyInfoExpression((PropertyInfo)memberExpression.Member));
                    }
                    curExpression = memberExpression.Expression;
                }
                else if (curExpression is MethodCallExpression)
                {
                    MethodCallExpression methodCallExpression = (MethodCallExpression)curExpression;

                    var method = methodCallExpression.Method;
                    if (method.IsSpecialName && method.Name.Equals("get_Item")) // Indexer-Getter
                    {
                        object[] arguments = null;
                        try
                        {
                            arguments = methodCallExpression.Arguments
                                                    .Cast<ConstantExpression>()
                                                    .Select(c => c.Value)
                                                    .ToArray();
                        }
                        catch (InvalidCastException)
                        {
                            throw new ArgumentException("Als Indexer-Argumente dürfen nur Konstanten vorkommen!", "propertyExpression");
                        }
                        memberInfoExpressionList.Insert(0, new IndexerMethodInfoExpression(method, arguments));
                    }
                    curExpression = methodCallExpression.Object;
                }
                else
                {
                    break;
                }
            }
            while (curExpression != null);

            if (!allowEmpty && memberInfoExpressionList.Count == 0)
                throw new ArgumentException("In der Expression dürfen nur Properties und Indexer-Methods enthalten sein", "propertyExpression");

            return memberInfoExpressionList;
        }

        /// <summary>
        /// Erstellt einen PropertyPath von einer Liste von PropertyInfos (z.B. "Person.Name")
        /// </summary>
        /// <param name="propertyInfos">PropertyInfo</param>
        /// <returns></returns>
        private static string BuildPropertyPath(IEnumerable<IMemberInfoExpression> propertyInfos)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerator<IMemberInfoExpression> propertyInfosEnumerator = propertyInfos.GetEnumerator();
            if (!propertyInfosEnumerator.MoveNext())
                return String.Empty;
            sb.Append(propertyInfosEnumerator.Current.ToString());

            while (propertyInfosEnumerator.MoveNext())
            {
                if (propertyInfosEnumerator.Current is PropertyInfoExpression)
                    sb.Append('.');
                sb.Append(propertyInfosEnumerator.Current.ToString());
            }

            return sb.ToString();
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// Interface für 
        /// </summary>
        private interface IMemberInfoExpression
        {
            /// <summary>
            /// Liefert den Rückgabetyp.
            /// </summary>
            Type ReturnType { get; }
        }

        /// <summary>
        /// Container für PropertyInfo.
        /// </summary>
        private class PropertyInfoExpression : IMemberInfoExpression
        {
            public PropertyInfoExpression(PropertyInfo propertyInfo)
            {
                PropertyInfo = propertyInfo;
            }

            public PropertyInfo PropertyInfo { get; private set; }

            #region IMemberInfoExpression Members

            /// <inheritdoc />
            public Type ReturnType
            {
                get { return PropertyInfo.PropertyType; }
            }

            #endregion

            public override string ToString()
            {
                return PropertyInfo.Name;
            }
        }

        /// <summary>
        /// Container für die MethodInfo von Indexer.
        /// </summary>
        private class IndexerMethodInfoExpression : IMemberInfoExpression
        {
            public IndexerMethodInfoExpression(MethodInfo methodInfo, object[] arguments)
            {
                MethodInfo = methodInfo;
                Arguments = arguments;
            }

            public object[] Arguments { get; private set; }

            public MethodInfo MethodInfo { get; private set; }

            #region IMemberInfoExpression Members

            /// <inheritdoc />
            public Type ReturnType
            {
                get { return MethodInfo.ReturnType; }
            }

            #endregion

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append('[');
                for (int i = 0; i < Arguments.Length; i++)
                {
                    if (i > 0)
                        sb.Append(',');
                    sb.Append(Arguments[i].ToString());
                }
                sb.Append(']');
                return sb.ToString();
            }
        }

        #endregion
    }
}