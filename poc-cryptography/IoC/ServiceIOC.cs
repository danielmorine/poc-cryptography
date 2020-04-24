using Microsoft.Extensions.DependencyInjection;
using poc_cryptography.Services.DecryptService;
using poc_cryptography.Services.FileService;
using poc_cryptography.Services.HttpRequestService;
using poc_cryptography.Services.SecurityService;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_cryptography.IoC
{
   public static class ServiceIoC
    {
        public static IServiceCollection ServiceInjection(this IServiceCollection services)
        {
            services.AddSingleton<IHttpRequestService, HttpRequestService>();
            services.AddSingleton<IJob, Job>();
            services.AddSingleton<IDecryptService, DecryptService>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddSingleton<IFileService, FileService>();

            return services;
        }
    }
}
