
namespace HilltopDigital.SimpleDAL
{
    public class StoreProcedureParameter
    {
        public string ParameterName  {get; private set;}
        public object Value { get; private set; }
        public StoreProcedureParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

    }
}