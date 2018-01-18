using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Songhay.ContentNegotiation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Songhay.ContentNegotiation.OutputFormatters
{
    public class VcardOutputFormatter : TextOutputFormatter
    {
        public VcardOutputFormatter()
        {
            /*
                You can’t do constructor dependency injection in a formatter class.
                For example, you can’t get a logger by adding a logger parameter to the constructor.
                To access services, you have to use the context object that gets passed
                into your methods. A code example below shows how to do this.
                [https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-formatters]
            */

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Contact).IsAssignableFrom(type)
                || typeof(IEnumerable<Contact>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            IServiceProvider serviceProvider = context.HttpContext.RequestServices;
            var logger = serviceProvider.GetService(typeof(ILogger<VcardOutputFormatter>)) as ILogger;

            var response = context.HttpContext.Response;

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<Contact>)
            {
                foreach (Contact contact in context.Object as IEnumerable<Contact>)
                {
                    FormatVcard(buffer, contact, logger);
                }
            }
            else
            {
                var contact = context.Object as Contact;
                FormatVcard(buffer, contact, logger);
            }
            return response.WriteAsync(buffer.ToString());
        }

        static void FormatVcard(StringBuilder buffer, Contact contact, ILogger logger)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendFormat($"N:{contact.LastName};{contact.FirstName}\r\n");
            buffer.AppendFormat($"FN:{contact.FirstName} {contact.LastName}\r\n");
            buffer.AppendFormat($"UID:{contact.ID}\r\n");
            buffer.AppendLine("END:VCARD");
            logger.LogInformation($"Writing {contact.FirstName} {contact.LastName}");
        }
    }
}
