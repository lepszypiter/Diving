using Diving.Application.AddClient;
using Diving.Application.AddInstructor;
using Diving.Application.GetInstructor;
using Diving.Application.ModifyInstructor;
using Diving.Application.ReadClients;
using Diving.Application.ReadCourses;
using Diving.Application.UpdateClient;
using Diving.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Diving.Application;

public static class DivingApplication
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ReadClientsQueryHandler>();
        services.AddScoped<AddClientCommandHandler>();
        services.AddScoped<UpdateClientCommandHandler>();
        services.AddScoped<GetInstructorsQueryHandler>();
        services.AddScoped<AddInstructorCommandHandler>();
        services.AddScoped<ModifyInstructorsCommandHandler>();
        services.AddScoped<ReadCoursesQueryHandler>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();

        var tt = typeof(DivingApplication);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(tt.Assembly));
    }
}
