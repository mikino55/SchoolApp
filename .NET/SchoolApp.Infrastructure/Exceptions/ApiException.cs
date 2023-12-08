using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Infrastructure.Exceptions;
public class ApiException : Exception
{
    public ApiException(HttpStatusCode code, string message, string friendlyMessage) : base(message)
    {
        Code = code;
        FriendlyMessage = friendlyMessage;
    }
    public ApiException(HttpStatusCode code, string message) : this (code, message, message) { }

    public HttpStatusCode Code { get; }
    public string FriendlyMessage { get; }
}
