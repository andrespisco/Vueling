using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VY.Person.Infraestructure.Contract
{
    public class OperationResult
    {
        //First i need is the list of ErrorObject

        public List<ErrorObject> Errors { get; set; } = new List<ErrorObject>();

        public void AddError(ErrorObject errorObject)
        {
            Errors.Add(new ErrorObject { Exception = errorObject.Exception, Code = errorObject.Code , Message = errorObject.Message});
        }

        public void AddError(int code, string message, Exception exception = null)
        {
            Errors.Add( new ErrorObject { Exception = exception, Code = code, Message = message });
        }

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }

        public bool HasException()
        {
           return Errors.Any(c => c.Exception != null);
        }

    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }

        public void SetResult(T result)
        {
            Result = result;
        }
    }
}
