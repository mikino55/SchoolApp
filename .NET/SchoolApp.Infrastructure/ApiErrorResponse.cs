using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Infrastructure;
public record ApiErrorResponse(string Message, string FriendlyMessage)
{
}
