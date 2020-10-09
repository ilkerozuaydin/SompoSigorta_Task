using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Middleware
{
    public static class ProposalLogMiddleware
    {
        public static void ConfigureProposalLogMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ProposalMiddleware>();
        }
    }
}
