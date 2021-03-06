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
 * NullableKeyComparer.cs                                 *
 *                                                        *
 * NullableKeyComparer class for C#.                      *
 *                                                        *
 * LastModified: Mar 29, 2018                             *
 * Authors: Ma Bingyao <andot@hprose.com>                 *
 *                                                        *
\**********************************************************/
using System.Collections.Generic;

namespace Hprose.Collections.Generic {
    public class NullableKeyComparer<T> : IComparer<NullableKey<T>> {
        private readonly IComparer<T> _comparer;

        public NullableKeyComparer(IComparer<T> comparer) => _comparer = comparer;

        public int Compare(NullableKey<T> x, NullableKey<T> y) => _comparer?.Compare(x.Value, y.Value) ?? x.CompareTo(y);
    }
}
