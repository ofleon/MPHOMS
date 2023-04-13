

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MPHOMS.Web.Hubs;

namespace MPHOMS.Web
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
            where T : IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }
    }
}
