using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Api
{
    public sealed class ApiException : Exception
    {
        //
        // Resumen:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message.
        //
        // Parámetros:
        //   message:
        //     The message that describes the error.
        public ApiException(string message) : base(message) { }

        //
        // Resumen:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message and a reference to the inner exception that is the cause of this exception.
        //
        // Parámetros:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public ApiException(string message, Exception innerException) : base(message, innerException) { }

        //
        // Resumen:
        //     Initializes a new instance of the System.Exception class.
        public ApiException()
        { }
    }
}
