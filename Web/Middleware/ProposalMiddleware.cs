using Business.Abstract;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web.Middleware
{
    public class ProposalMiddleware
    {
        private readonly RequestDelegate _next;
        public ProposalMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext httpContext, IProposalService proposalService)
        {
            try
            {
                if (httpContext.Request.Path.Value == "/Home/GetProposal")
                {

                    var jsonRequestString = await FormatRequest(httpContext.Request);
                    AddProposalLogRequest addLogRequest = JsonConvert.DeserializeObject<AddProposalLogRequest>(jsonRequestString);
                    var addLogResult = await proposalService.Add(addLogRequest);

                    var originalBodyStream = httpContext.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {
                        httpContext.Response.Body = responseBody;

                        await _next(httpContext);

                        var jsonResponseString = await FormatResponse(httpContext.Response);
                        var response = JsonConvert.DeserializeObject<GetProposalResponse>(jsonResponseString);
                        var updateLogRequest = new UpdateProposalLogRequest
                        {
                            Id = addLogResult.Data.Id,
                            Response = JsonConvert.SerializeObject(response)
                        };
                        await proposalService.Update(updateLogRequest);

                        await responseBody.CopyToAsync(originalBodyStream);
                    }




                }
                else
                {
                    await _next(httpContext);
                }

            }
            catch (Exception e)
            {
                //await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering(bufferThreshold: 1024 * 45, bufferLimit: 1024 * 100);
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            return requestBody;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {

            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return responseBody;
        }
    }
}
