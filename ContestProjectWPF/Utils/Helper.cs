using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ContestProjectWPF.Utils
{
    public class Helper
    {
        private List<string> listPrim;

        public Helper()
        {
            listPrim = new List<string>();
        }

        public bool IsPrim(string input)
        {
            if (listPrim.Contains(input))
                return true;
            else
            {
                long inp = long.Parse(input);
                return IsPrim(inp);
            }
        }

        public bool IsPrim(long input)
        {
            for (long a = 2; a < input; a++)
            {
                if (input % a == 0)
                {
                    return false;
                }
            }

            if (input != 1)
            {
                string str = input.ToString();
                if (!listPrim.Contains(str))
                    listPrim.Add(str);
                return true;
            }
            else
                return false;
        }

        public static string ListToString(List<string> list)
        {
            return ListToString(list, " ", String.Empty);
        }

        public static string ListToString(List<string> list, string seperator)
        {
            return ListToString(list, seperator, String.Empty);
        }

        public static string ListToString(List<string> list, string seperator, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            foreach (string item in list)
            {
                if (!isFirst)
                    sb.Append(seperator);
                else
                    isFirst = false;

                sb.Append(delimiter);
                sb.Append(item);
                sb.Append(delimiter);
            }
            return sb.ToString();
        }

        public static List<string> StringToList(string input, string seperator = " ", string delimiter = "")
        {
            string splitter = delimiter + seperator + delimiter;
            if (delimiter.Length > 0)
                input = input.Substring(delimiter.Length, input.Length - 2 * delimiter.Length);

            return (new List<string>(Regex.Split(input, seperator)));
        }

        public static List<T> StringToList<T>(string input, string seperator = " ", string delimiter = "")
        {
            string splitter = delimiter + seperator + delimiter;
            if (delimiter.Length > 0)
                input = input.Substring(delimiter.Length, input.Length - 2 * delimiter.Length);

            return Regex.Split(input, seperator).Select(s => Parse<T>(s)).ToList();
        }

        public static object StringToList(Type type, string input, string seperator = " ", string delimiter = "")
        {
            string splitter = delimiter + seperator + delimiter;
            if (delimiter.Length > 0)
                input = input.Substring(delimiter.Length, input.Length - 2 * delimiter.Length);

            Type listType = typeof(List<>);
            Type genericListType = listType.MakeGenericType(type);
            var liste = Activator.CreateInstance(genericListType);

            foreach (object value in Regex.Split(input, seperator).Select(s => Parse(s, type)))
            {
                liste.GetType().GetMethod("Add").Invoke(liste, new object[] { value });
            }

            return liste;
        }

        public static T Parse<T>(string s)
        {
            //return (T)Convert.ChangeType(s, typeof(T));
            return (T)Parse(s, typeof(T));
        }

        /// <summary>
        /// Versucht den Wert (meistens Zeichenkette) in ein bestimmten Zieltyp zu parsen.
        /// Liefert den ResultType und den geparsten Wert zurück.
        /// </summary>
        /// <param name="value">Wert, der geparst werden soll.</param>
        /// <param name="destinationType">Typ, in den der Wert geparst werden soll.</param>
        /// <param name="parsedValue">Geparster Wert</param>
        /// <param name="exception">Exception</param>
        /// <returns>ResultType</returns>
        public static object Parse(object value, Type destinationType)
        {
            object parsedValue = null;

            Type concreteDestinationType = ((destinationType.IsGenericType) && (destinationType.GetGenericTypeDefinition() == (typeof(Nullable<>)))) ? destinationType.GetGenericArguments()[0] : destinationType;

            if (IsAssignableTo(value, destinationType))
            {
                parsedValue = value;
            }
            else if (NullableCheck(value, destinationType))
            {
                parsedValue = null;
            }
            else if (value is IConvertible)
            {
                if (!IsNullableType(destinationType) && (value == null || ((value is string) && (String.IsNullOrWhiteSpace((string)value)))))
                    parsedValue = Activator.CreateInstance(destinationType);
                else
                    parsedValue = Convert.ChangeType(value, concreteDestinationType);
            }
            else
                throw new ArgumentException();

            return parsedValue;
        }

        /// <summary>
        /// Prüft, ob es sich bei dem Ziel-Typ um ein Nullable-Datentyp handelt und prüft den Wert gegen.
        /// </summary>
        /// <param name="value">Wert</param>
        /// <param name="destinationType">Ziel-Typ</param>
        /// <returns>
        ///    TRUE, falls der Wert nach NULL konvertiert werden kann und der Datentyp ist ein Nullable-Typ.
        ///    FALSE, falls der Wert nicht NULL ist oder der Datentyp kein Nullable-Typ ist.
        /// </returns>
        private static bool NullableCheck(object value, Type destinationType)
        {
            Type nullableNestedType = null;
            if (IsNullableType(destinationType))
                nullableNestedType = destinationType.GetGenericArguments()[0];

            if (((nullableNestedType != null) || (destinationType.IsClass) || (destinationType.IsInterface)) &&    // Wenn der Zieltyp ein Nullable-Wert oder ein Class-Typ oder ein Interface-Typ ist, dann
                ((value == null) || ((value is string) && (String.IsNullOrEmpty((string)value)))))  // -darf der Wert NULL oder ein Leerstring sein
                return true;
            else
                return false;
        }

        /// <summary>
        /// Prüft, ob der Wert in den Ziel-Typ castable ist.
        /// </summary>
        /// <param name="value">Wert</param>
        /// <param name="destinationType">Ziel-Typ</param>
        /// <returns>
        ///    TRUE, falls der Wert nach NULL konvertiert werden kann und der Datentyp ist ein Nullable-Typ.
        ///    FALSE, falls der Wert nicht NULL ist oder der Datentyp kein Nullable-Typ ist.
        /// </returns>
        private static bool IsAssignableTo(object value, Type destinationType)
        {
            Type nullableNestedType = null;
            if (IsNullableType(destinationType))
                nullableNestedType = destinationType.GetGenericArguments()[0];
            Type concreteDestinationType = nullableNestedType != null ? nullableNestedType : destinationType;

            if (((value != null) &&
                   ((concreteDestinationType == value.GetType()) ||                                                      // Der Typ des Wertes entspricht dem Ziel-Typ 
                   (destinationType.IsAssignableFrom(value.GetType())) ||                                                // Der Typ ist mit dem Zieltyp kompatibel (Ableitung)
                   (destinationType.IsInterface && value.GetType().GetInterface(destinationType.FullName) != null))) ||  // Der Typ ist mit dem Zieltyp kompatibel (Interface)
                ((value == null) &&
                   ((nullableNestedType != null) || (destinationType.IsClass))))
                return true;
            else
                return false;
        }

        private static bool IsNullableType(Type destinationType)
        {
            return destinationType.IsGenericType && (destinationType.GetGenericTypeDefinition() == (typeof(Nullable<>)));
        }

    }
}
