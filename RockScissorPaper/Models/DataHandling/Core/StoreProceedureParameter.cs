using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class StoreProceedureParameter
    {
        public string ParameterName  {get; private set;}
        public object Value { get; private set; }
        public StoreProceedureParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

    }
}