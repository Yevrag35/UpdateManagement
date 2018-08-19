using Microsoft.UpdateServices.Internal.DatabaseAccess;
using Microsoft.UpdateServices.Internal;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace MG.UpdateManagement.Framework
{
    [Serializable]
    public class ReadableRow : IReadableRow, IDataRecord
    {
        // Fields
        private object[] values;

        // Methods
        public ReadableRow() => values = new object[0];

        public ReadableRow(params object[] values) : this() => values = values ?? throw new ArgumentNullException("values");

        public bool GetBoolean(int index) => values[index] == null ? false : Convert.ToBoolean(values[index]);

        public byte GetByte(int index) => values[index] == null ? (byte)0 : (byte)values[index];

        public long GetBytes(int index, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (values[index] == null)
            {
                return 0L;
            }
            byte[] sourceArray = (byte[])values[index];
            if (buffer == null)
            {
                return (long)sourceArray.Length;
            }
            long num = sourceArray.Length - fieldOffset;
            if (num < 0L)
            {
                num = 0L;
            }
            if (num > length)
            {
                num = length;
            }
            if (num > 0L)
            {
                Array.Copy(sourceArray, fieldOffset, buffer, (long)bufferOffset, num);
            }
            return num;
        }

        public char GetChar(int i) => throw new NotImplementedException();

        public long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length) => throw new NotImplementedException();

        public IDataReader GetData(int i) => throw new NotImplementedException();

        public string GetDataTypeName(int i) => throw new NotImplementedException();

        public DateTime GetDateTime(int index) => 
            values[index] == null ? DateTime.MinValue : DateTimeUtilities.ToUtcIfLocal(Convert.ToDateTime(values[index]));

        public decimal GetDecimal(int i) => throw new NotImplementedException();

        public double GetDouble(int i) => throw new NotImplementedException();

        public Type GetFieldType(int i) => throw new NotImplementedException();

        public float GetFloat(int i) => throw new NotImplementedException();

        public Guid GetGuid(int index) => values[index] == null ? Guid.Empty : (Guid)values[index];

        public short GetInt16(int index) => values[index] == null ? (short)0 : (short)values[index];

        public int GetInt32(int index) => values[index] == null ? 0 : (int)values[index];

        public long GetInt64(int index)
        {
            object obj2 = values[index];
            return values[index] == null ? 0L : obj2.GetType() == typeof(int) ? (long)((int)obj2) : (long)obj2;
        }

        public string GetName(int i) => throw new NotImplementedException();

        public int GetOrdinal(string s) => throw new NotImplementedException();

        public DataTable GetSchemaTable() => throw new NotImplementedException();

        public string GetString(int index) => values[index] == null ? string.Empty : (string)values[index];

        public object GetValue(int index) =>
            values[index];

        public int GetValues(object[] o) => throw new NotImplementedException();

        public bool IsDBNull(int index) => values[index] == null;

        public void ReadRow(IDataRecord record)
        {
            if (record == null)
            {
                throw new ArgumentNullException("record");
            }
            values = new object[record.FieldCount];
            for (int i = 0; i < record.FieldCount; i++)
            {
                object obj2 = record.GetValue(i);
                if (obj2 == DBNull.Value)
                {
                    obj2 = null;
                }
                values[i] = obj2;
            }
        }

        // Properties
        public object[] Values
        {
            get => values;
            set => values = value;
        }

        public int FieldCount => values.Length;

        public object this[int index]
        {
            get => GetValue(index);
            set => values[index] = value;
        }

        public object this[string index] => throw new NotImplementedException();
    }
}
