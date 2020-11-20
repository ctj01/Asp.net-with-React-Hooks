using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.ManejadorErr
{
    public class ErrorHandler:Exception
    {
        public HttpStatusCode Codigo { get; set; }
        public object Error { get; set; }

        public ErrorHandler(HttpStatusCode Code, object Err)
        {
            this.Codigo = Code;
            this.Error = Err;
        }
    }
}
