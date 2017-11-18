using System;
using System.Collections.Generic;

namespace Fme.Library.Comparison
{
    public interface IGenericConverter
    {
        bool Compare(object value, Func<object, bool> selector);
        object Convert(string value);
        List<object> Convert(string[] values);
        string Join(object[] values, Func<object, object> selector);
        string Transform(string value, Func<object, object> selector);
    }
    public interface IGenericConverter<T>
    {
        bool Compare(T value, Func<T, bool> selector);
        T Convert(string value);
        List<T> Convert(string[] values);
        string Join(T[] values, Func<T, T> selector);
        string Transform(string value, Func<T, T> selector);
    }
}