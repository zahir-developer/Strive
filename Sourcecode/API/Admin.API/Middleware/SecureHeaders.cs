using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OwaspHeaders.Core.Models;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Enums;

namespace Admin.API.Middleware
{
    public class SecureHeaders
    {
        public static SecureHeadersMiddlewareConfiguration CustomConfiguration()
        {
            return SecureHeadersMiddlewareBuilder
                .CreateBuilder()
                .UseHsts(1200, false)
                 .UseXSSProtection(XssMode.oneReport, "https://reporturi.com/some-report-url")
                .UseContentDefaultSecurityPolicy()
                .UsePermittedCrossDomainPolicies(XPermittedCrossDomainOptionValue.masterOnly)
                .UseReferrerPolicy(ReferrerPolicyOptions.sameOrigin)
                .Build();
        }
    }
}
