using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

//------------------------------------------------------------------------
//	RZL KIS                                                    (c)2011 RZL
//
//
//	MS-Windows										    (w) Thomas Humer
//------------------------------------------------------------------------
//  ObservableObject.cs
//------------------------------------------------------------------------
namespace ContestProject.Utils
{
    /// <summary>
    /// Basisklasse mit Unterstützung, um NotifyPropertyChanged-Events zu werfen.
    /// </summary>
    [Serializable]
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Konstruktor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ObservableObject()
        {
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// <see cref="INotifyPropertyChanged"/>
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Wirft das PropertyChanged-Event.
        /// </summary>
        /// <param name="e">PropertyChangedEventArgs</param>        
        private void OnPropertyChangedIntern(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);

            OnPropertyChanged(e);
        }

        /// <summary>
        /// Wird direkt nach dem PropertyChanged-Event aufgerufen.
        /// </summary>
        /// <param name="e">PropertyChangedEventArgs</param>
        /// <returns></returns>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Wirft das PropertyChanged-Event.
        /// </summary>
        /// <param name="propertyName">PropertyName</param>
        private void RaisePropertyChangedIntern(string propertyName)
        {
            OnPropertyChangedIntern(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Wirft das PropertyChanged-Event.
        /// </summary>
        /// <param name="propertyName">PropertyName</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChangedIntern(propertyName);
        }

        /// <summary>
        /// Wirft das PropertyChanged-Event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = ExpressionHelper.ExtractPropertyName(propertyExpression);
            RaisePropertyChangedIntern(propertyName);
        }

        /// <summary>
        /// Wirft das PropertyChanged-Event.
        /// </summary>
        /// <typeparam name="T">The type of the property that has a new value</typeparam>
        /// <param name="propertyExpression">A Lambda expression representing the property that has a new value.</param>
        internal void RaisePropertyChangedIntern<TEntity, T>(Expression<Func<TEntity, T>> propertyExpression)
        {
            var propertyName = ExpressionHelper.ExtractPropertyName(propertyExpression);
            RaisePropertyChangedIntern(propertyName);
        }

        /// <summary>
        /// Wird direkt nach dem PropertyChanging-Event aufgerufen.
        /// </summary>
        /// <param name="e">PropertyChangedEventArgs</param>
        /// <param name="newValue">Neuer Wert</param>
        /// <returns></returns>
        protected virtual void OnPropertyChanging<T>(PropertyChangedEventArgs e, T newValue)
        {
        }

        /// <summary>
        /// Wird direkt nach dem PropertyChanging-Event aufgerufen.
        /// </summary>
        /// <param name="e">PropertyChangedEventArgs</param>
        /// <param name="newValue">Neuer Wert</param>
        /// <returns></returns>
        protected void PropertyChanging<T>(Expression<Func<T>> propertyExpression, T newValue)
        {
            var propertyName = ExpressionHelper.ExtractPropertyName(propertyExpression);
            OnPropertyChanging<T>(new PropertyChangedEventArgs(propertyName), newValue);
        }

        #endregion
    }
}