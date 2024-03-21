using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IRepository;
using Talabat.Errors;
using Talabat.Helpers;
using Talabat.Repository;

namespace Talabat.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                    .SelectMany(P => P.Value.Errors).Select(E => E.ErrorMessage).ToArray();
                    var validationErroeResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErroeResponse);
                };
              
            });
            return services;
        }
    }
}
