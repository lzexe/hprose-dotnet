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
 * MultiDimArraySerializer.cs                             *
 *                                                        *
 * MultiDimArraySerializer class for C#.                  *
 *                                                        *
 * LastModified: Jan 11, 2019                             *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/

using System;

namespace Hprose.IO.Serializers {
    using static Tags;

    class MultiDimArraySerializer<T> : ReferenceSerializer<T> {
        public override void Write(Writer writer, T obj) {
            base.Write(writer, obj);
            Array array = (Array)(object)obj;
            int rank = array.Rank;
            int i;
#if NETCOREAPP2_1 || NETCOREAPP2_2
            Span<int> lb = stackalloc int[rank];
            Span<int> ub = stackalloc int[rank];
            Span<int> len = stackalloc int[rank];
#else
            int[] lb = new int[rank];
            int[] ub = new int[rank];
            int[] len = new int[rank];
#endif
            int[] loc = new int[rank];
            int maxrank = rank - 1;
            for (i = 0; i < rank; ++i) {
                lb[i] = array.GetLowerBound(i);
                ub[i] = array.GetUpperBound(i);
                len[i] = array.GetLength(i);
                loc[i] = lb[i];
            }
            var stream = writer.Stream;
            stream.WriteByte(TagList);
            if (len[0] > 0) ValueWriter.WriteInt(stream, len[0]);
            stream.WriteByte(TagOpenbrace);
            var serializer = Serializer.Instance;
            while (loc[0] <= ub[0]) {
                int n = 0;
                for (i = maxrank; i > 0; i--) {
                    if (loc[i] == lb[i]) {
                        n++;
                    }
                    else {
                        break;
                    }
                }
                for (i = rank - n; i < rank; ++i) {
                    writer.SetReference(new object());
                    stream.WriteByte(TagList);
                    if (len[i] > 0) ValueWriter.WriteInt(stream, len[i]);
                    stream.WriteByte(TagOpenbrace);
                }
                for (loc[maxrank] = lb[maxrank];
                     loc[maxrank] <= ub[maxrank];
                     loc[maxrank]++) {
                    serializer.Serialize(writer, array.GetValue(loc));
                }
                for (i = maxrank; i > 0; i--) {
                    if (loc[i] > ub[i]) {
                        loc[i] = lb[i];
                        loc[i - 1]++;
                        stream.WriteByte(TagClosebrace);
                    }
                }
            }
            stream.WriteByte(TagClosebrace);
        }
    }
}
