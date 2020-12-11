using A35Mge.Database;
using A35Mge.IDS.Ids;
using A35Mge.IDS.ValidateExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.IDS
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
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            var config = Configuration.GetSection("Connection");
            services.AddDbContext<A35MgeDbContext>(options => options.UseMySql(config?.Value ?? string.Empty, mysql =>
            {
                var builder = mysql
                 .MigrationsAssembly(System.Reflection.Assembly.Load("A35Mge.MySqlDatabase").FullName)
                 .EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
            }));
            services.AddIdentityServer(options =>
            {
                //可以通过此设置来指定登录路径，默认的登陆路径是/account/login
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
             //添加证书加密方式，执行该方法，会先判断tempkey.rsa证书文件是否存在，如果不存在的话，就创建一个新的tempkey.rsa证书文件，如果存在的话，就使用此证书文件。
             .AddDeveloperSigningCredential()
             //把受保护的Api资源添加到内存中
             .AddInMemoryApiResources(IdsConfig.GetApiResources())
             //身份信息资源
             .AddInMemoryIdentityResources(IdsConfig.GetIdentityResources())
             //添加自定义客户端
             .AddClientStore<ClientStore>()
             //添加自定义账号密码的方法
             .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
             //新增微信认证登陆的方法
             //.AddExtensionGrantValidator<WechatLoginValidator>()
             .AddInMemoryApiScopes(IdsConfig.ApiScopes);
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
