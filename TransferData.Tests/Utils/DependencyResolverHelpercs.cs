﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransferData.Tests.Utils
{
    public class DependencyResolverHelpercs
    {
        private readonly IWebHost _webHost;

        /// <inheritdoc />
        public DependencyResolverHelpercs(IWebHost WebHost) => _webHost = WebHost;

        public T GetService<T>()
        {
            //   using (
            var serviceScope = _webHost.Services.CreateScope();
            //{
            var services = serviceScope.ServiceProvider;

            try
            {
                var scopedService = services.GetRequiredService<T>();
                return scopedService;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            // };
        }
    }
}
