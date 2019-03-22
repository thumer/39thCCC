using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ContestProject.Utils
{
    /// <summary>
    /// Basisklasse mit Unterstützung, um NotifyPropertyChanged-Events zu werfen.
    /// </summary>
    [Serializable]
    public class ObservableObject : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Wirft das PropertyChanged-Event.
        /// </summary>
        /// <param name="propertyName">PropertyName</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static class ObservableObjectExtensions
    {
        private static readonly Action<ObservableObject, string> _raisePropertyChangedMethod;

        static ObservableObjectExtensions()
            => _raisePropertyChangedMethod = (Action<ObservableObject, string>)typeof(ObservableObject)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name is "RaisePropertyChanged")
            .First(m => m.GetParameters().Length == 2)
            .CreateDelegate(typeof(Action<ObservableObject, string>));

        public static bool Set<T>(this ObservableObject @this, ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field == null || !EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                _raisePropertyChangedMethod(@this, propertyName);
                return true;
            }
            return false;
        }
    }
}