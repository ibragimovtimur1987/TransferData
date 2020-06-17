using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TransferData.DAL.EF;
using TransferData.DAL.Repositories;
using TransferData.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TransferData.DAL.Models;

namespace TransferData.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            if (connection == null)
            {
                connection = "Server=(localdb)\\mssqllocaldb;Database=TransferDataDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            }
            services.AddDbContext<TransferDataContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddScoped<IGenericRepository<ExcelModel1>, Excel1Repository>();
            services.AddScoped<IGenericRepository<ExcelModel2>, Excel2Repository>();
            services.AddScoped <Q101.ExcelLoader.Abstract.IExcelFileLoader, Q101.ExcelLoader.Concrete.ExcelFileLoader>();
            services.AddScoped<BLL.Services.Interface.ITransferExcelService, BLL.Services.TransferExcelService>();
           // services.AddScoped<BLL.Infrastructure.IAutoMapper, BLL.Infrastructure.AutoMapperAdapter>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
