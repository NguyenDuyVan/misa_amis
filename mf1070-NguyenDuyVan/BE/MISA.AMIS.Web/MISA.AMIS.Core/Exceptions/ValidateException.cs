using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Exceptions
{
    public class ValidateException : Exception
    {
        public string ErrorMsg { get; set; }
        IDictionary ErrorData;
        public ValidateException(string errorMsg, IDictionary errorData)
        {
            this.ErrorMsg = errorMsg;
            ErrorData = errorData;
        }

        public override string Message => this.ErrorMsg;
        public override IDictionary Data => this.ErrorData;
    }
}
