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
 * CharsDeserializer.cs                                   *
 *                                                        *
 * CharsDeserializer class for C#.                        *
 *                                                        *
 * LastModified: Jan 11, 2019                             *
 * Author: Ma Bingyao <andot@hprose.com>                  *
 *                                                        *
\**********************************************************/

namespace Hprose.IO.Deserializers {
    using static Tags;

    class CharsDeserializer : Deserializer<char[]> {
        private static readonly char[] empty = new char[0] { };
        public override char[] Read(Reader reader, int tag) {
            var stream = reader.Stream;
            switch (tag) {
                case TagString:
                    return ReferenceReader.ReadChars(reader);
                case TagUTF8Char:
                    return new char[] { ValueReader.ReadChar(stream) };
                case TagList:
                    return ReferenceReader.ReadArray<char>(reader);
                case TagEmpty:
                    return empty;
                case TagTrue:
                    return bool.TrueString.ToCharArray();
                case TagFalse:
                    return bool.FalseString.ToCharArray();
                case TagNaN:
                    return double.NaN.ToString().ToCharArray();
                case TagInfinity:
                    return ValueReader.ReadInfinity(stream).ToString().ToCharArray();
                case TagInteger:
                case TagLong:
                case TagDouble:
                    return Converter<char[]>.Convert(ValueReader.ReadUntil(stream, TagSemicolon));
                case TagDate:
                    return Converter<char[]>.Convert(ReferenceReader.ReadDateTime(reader));
                case TagTime:
                    return Converter<char[]>.Convert(ReferenceReader.ReadTime(reader));
                case TagGuid:
                    return Converter<char[]>.Convert(ReferenceReader.ReadGuid(reader));
                case TagBytes:
                    return Converter<char[]>.Convert(ReferenceReader.ReadBytes(reader));
                default:
                    if (tag >= '0' && tag <= '9') {
                        return new char[] { (char)tag };
                    }
                    return base.Read(reader, tag);
            }
        }
    }
}
