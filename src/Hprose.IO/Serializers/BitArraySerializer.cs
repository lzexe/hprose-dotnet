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
 * BitArraySerializer.cs                                  *
 *                                                        *
 * BitArraySerializer class for C#.                       *
 *                                                        *
 * LastModified: Jan 11, 2019                             *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/

using System.Collections;

namespace Hprose.IO.Serializers {
    using static Tags;

    class BitArraySerializer : ReferenceSerializer<BitArray> {
        public override void Write(Writer writer, BitArray obj) {
            base.Write(writer, obj);
            var stream = writer.Stream;
            int length = obj.Length;
            stream.WriteByte(TagList);
            if (length > 0) {
                ValueWriter.WriteInt(stream, length);
            }
            stream.WriteByte(TagOpenbrace);
            var serializer = Serializer<bool>.Instance;
            for (int i = 0; i < length; ++i) {
                serializer.Serialize(writer, obj[i]);
            }
            stream.WriteByte(TagClosebrace);
        }
    }
}
