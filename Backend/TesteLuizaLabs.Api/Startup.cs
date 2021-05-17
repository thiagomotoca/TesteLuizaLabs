using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TesteLuizaLabs.Api.Helpers.Filters;
using TesteLuizaLabs.Aplicacao.Interfaces.Repositorio;
using TesteLuizaLabs.Aplicacao.Interfaces.Servico;
using TesteLuizaLabs.Aplicacao.Models;
using TesteLuizaLabs.Aplicacao.Repositorio;
using TesteLuizaLabs.Aplicacao.Repositorio.Contexto;
using TesteLuizaLabs.Aplicacao.Servicos;

namespace TesteLuizaLabs.Api
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
            services.AddCors();

            services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                    opt.JsonSerializerOptions.IgnoreNullValues = true;
                });

            // services.AddMvc(options =>
            // {
            //     options.Filters.Add(new ExceptionFilter());
            // });
            services.AddMvc();

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<EntityFrameworkContext>(options =>
                options.UseInMemoryDatabase(databaseName: "TesteLuizaLabs")
            );

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Teste Luizalabs API", Version = "v1" });
                c.IncludeXmlComments(GetXmlCommentsPath());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header.<br>Coloque 'Bearer {TOKEN}'.<br>Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            RegisterDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste Luizalabs API"));

            app.UseRouting();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string GetXmlCommentsPath()
        {
            var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
            var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
            var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
            return caminhoXmlDoc;
        }

        private IServiceCollection RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IServicoUsuario, ServicoUsuario>();

            services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();

            return services;
        }
    }
}
