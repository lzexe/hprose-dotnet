﻿/**********************************************************\
|                                                          |
|                          hprose                          |
|                                                          |
| Official WebSite: http://www.hprose.com/                 |
|                   http://www.hprose.org/                 |
|                                                          |
\**********************************************************/
/**********************************************************\
 *                                                        *
 * DateTimeConverter.cs                                   *
 *                                                        *
 * hprose DateTimeConverter class for C#.                 *
 *                                                        *
 * LastModified: Apr 21, 2018                             *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/

using System;
using System.Text;

namespace Hprose.IO.Converters {
    static class DateTimeConverter {
        static DateTimeConverter() {
            Converter<bool, DateTime>.convert = Convert.ToDateTime;
            Converter<char, DateTime>.convert = Convert.ToDateTime;
            Converter<byte, DateTime>.convert = Convert.ToDateTime;
            Converter<sbyte, DateTime>.convert = Convert.ToDateTime;
            Converter<short, DateTime>.convert = Convert.ToDateTime;
            Converter<ushort, DateTime>.convert = Convert.ToDateTime;
            Converter<int, DateTime>.convert = Convert.ToDateTime;
            Converter<uint, DateTime>.convert = Convert.ToDateTime;
            Converter<long, DateTime>.convert = (value) => new DateTime(value);
            Converter<ulong, DateTime>.convert = Convert.ToDateTime;
            Converter<float, DateTime>.convert = Convert.ToDateTime;
            Converter<double, DateTime>.convert = Convert.ToDateTime;
            Converter<decimal, DateTime>.convert = Convert.ToDateTime;
            Converter<string, DateTime>.convert = (value) => DateTime.Parse(value);
            Converter<StringBuilder, DateTime>.convert = (value) => DateTime.Parse(value.ToString());
            Converter<char[], DateTime>.convert = (value) => DateTime.Parse(new string(value));
            Converter<object, DateTime>.convert = (value) => {
                switch (value) {
                    case DateTime dt:
                        return dt;
                    case string s:
                        return DateTime.Parse(s);
                    case char[] chars:
                        return DateTime.Parse(new string(chars));
                    case StringBuilder sb:
                        return DateTime.Parse(sb.ToString());
                    case long l:
                        return new DateTime(l);
                    default:
                        return Converter<DateTime>.ConvertFromObject(value);
                }
            };
        }
        internal static void Initialize() { }
    }
}
