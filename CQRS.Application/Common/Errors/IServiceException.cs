using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Common.Errors
{
    public interface IServiceException
    {
        public HttpStatusCode statusCodes { get; }
        public string Errormessage { get; }
    }
}
