using System;
using System.Collections.Generic;

namespace AgilityWall.Core.Infrastructure
{
    public class FuncComparer<T, TProperty> : 
        IComparer<T> where TProperty : IComparable, IEquatable<TProperty>
    {
        private readonly Func<T, TProperty> _getComparisonKey;
        private readonly bool _asc;

        /// <summary>
        /// Creates a new instance of PropertyComparer.
        /// </summary>
        public FuncComparer(Func<T, TProperty> getComparisonKey, bool asc = true)
        {
            _getComparisonKey = getComparisonKey;
            _asc = asc;
        }

        public int GetHashCode(T obj)
        {
            //get the value of the comparison property out of obj
            object propertyValue = _getComparisonKey(obj);

            if (propertyValue == null)
                return 0;

            return propertyValue.GetHashCode();
        }

        public int Compare(T x, T y)
        {
            var xValue = _getComparisonKey(x);
            var yValue = _getComparisonKey(y);

            if (!_asc)
            {
                xValue = yValue;
                yValue = _getComparisonKey(x);
            }

            if ((object)xValue == (object)yValue) return 0;

            if (xValue == null)
                return -1;

            return ((IComparable) xValue).CompareTo(yValue);
        }
    }

    /// <summary>
    /// Insired from: http://www.codeproject.com/Articles/94272/A-Generic-IEqualityComparer-for-Linq-Distinct
    /// </summary>
    public class FuncEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> _getComparisonKey;

        /// <summary>
        /// Creates a new instance of PropertyComparer.
        /// </summary>
        public FuncEqualityComparer(Func<T, object> getComparisonKey)
        {
            _getComparisonKey = getComparisonKey;
        }

        public bool Equals(T x, T y)
        {
            //get the current value of the comparison property of x and of y
            object xValue = _getComparisonKey(x);
            object yValue = _getComparisonKey(y);

            //if the xValue is null then we consider them equal if and only if yValue is null
            if (xValue == null)
                return yValue == null;

            //use the default comparer for whatever type the comparison property is.
            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            //get the value of the comparison property out of obj
            object propertyValue = _getComparisonKey(obj);

            if (propertyValue == null)
                return 0;

            return propertyValue.GetHashCode();
        }
    }
}