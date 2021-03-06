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
 * NullableKey.cs                                         *
 *                                                        *
 * NullableKey class for C#.                              *
 *                                                        *
 * LastModified: Mar 29, 2018                             *
 * Authors: Ma Bingyao <andot@hprose.com>                 *
 *                                                        *
\**********************************************************/
using System;
using System.Collections.Generic;

namespace Hprose.Collections.Generic {
    public struct NullableKey<T> : IComparable<NullableKey<T>>, IEquatable<NullableKey<T>> {
        private readonly T _value;

        public NullableKey(T value) => _value = value;

        public T Value => _value;

        public int CompareTo(NullableKey<T> other) => Comparer<T>.Default.Compare(_value, other._value);

        public override bool Equals(object obj) => obj is NullableKey<T> && Equals((NullableKey<T>)obj);

        public bool Equals(NullableKey<T> other) => _value?.Equals(other._value) ?? other._value == null;

        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        public override string ToString() => _value?.ToString() ?? "<null>";

        public static implicit operator T(NullableKey<T> key) => key._value;

        public static implicit operator NullableKey<T>(T value) => new NullableKey<T>(value);

        public static bool operator ==(NullableKey<T> x, NullableKey<T> y) => x.Equals(y);

        public static bool operator !=(NullableKey<T> x, NullableKey<T> y) => !x.Equals(y);
    }
}
