using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class MappingHelper
    {
        private DataRow dr;

        public MappingHelper()
        {
        }

        public MappingHelper(DataRow dataRow)
        {
            dr = dataRow;
        }

        public string MapString(String columnName)
        {
            String s = string.Empty;
            bool hasColumnName = dr.Table.Columns.Contains(columnName);

            if (hasColumnName)
            {
                s = Convert.ToString(dr[columnName]);
            }
            return s;
        }

        public int MapInt32(String columnName)
        {
            int i = default(int);
            bool hascolumnName = dr.Table.Columns.Contains(columnName);
            if (hascolumnName)
            {
                i = Convert.ToInt32(dr[columnName]);
            }
            return i;
        }

        public byte MapByte(String columnName)
        {
            byte i = default(byte);
            bool hascolumnName = dr.Table.Columns.Contains(columnName);
            if (hascolumnName)
            {
                i = Convert.ToByte(dr[columnName]);
            }
            return i;
        }

        public decimal MapDec(String columnName)
        {
            decimal d = 0;
            bool hascolumnName = dr.Table.Columns.Contains(columnName);
            if (hascolumnName)
            {
                d = Convert.ToDecimal(dr[columnName]);
            }
            return d;
        }

        public DateTime MapDateTime(String columnName)
        {
            DateTime dt = DateTime.Now;
            bool hascolumnName = dr.Table.Columns.Contains(columnName);
            if (hascolumnName)
            {
                dt = Convert.ToDateTime(dr[columnName]);
            }
            return dt;
        }

        public string MapDateText(String columnName)
        {
            String dx = string.Empty;
            bool hascolumnName = dr.Table.Columns.Contains(columnName);
            if (hascolumnName)
            {
                dx = Convert.ToString(dr["dd/mm/yyyy"]);
            }
            return dx;
        }

        public bool MapBool(String columnName)
        {
            bool b = true;
            bool hascolumnName = dr.Table.Columns.Contains(columnName);
            if (hascolumnName)
            {
                b = Convert.ToBoolean(dr[columnName]);
            }
            return b;
        }
    }
}