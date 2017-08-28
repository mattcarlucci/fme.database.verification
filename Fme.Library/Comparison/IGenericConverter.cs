using System;
using System.Collections.Generic;

namespace Fme.Library.Comparison
{
    public interface IGenericConverter<T>
    {
        bool Compare(T value, Func<T, bool> selector);
        T Convert(string value);
        List<T> Convert(string[] values);
        string Join(T[] values, Func<T, T> selector);
        string Transform(string value, Func<T, T> selector);
    }
}