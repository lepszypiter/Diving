﻿using Diving.Application.AddClient;
using Diving.Application.AddInstructor;
using Diving.Application.GetClients;
using Diving.Application.GetCourse;
using Diving.Application.GetInstructor;
using Diving.Application.ModifyClients;
using Diving.Application.ModifyCourse;
using Diving.Application.ModifyInstructor;
using Microsoft.Extensions.DependencyInjection;

namespace Diving.Application;

public static class DivingApplication
{
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<GetClientsQueryHandler>();
        services.AddScoped<AddClientCommandHandler>();
        services.AddScoped<ModifyClientsCommandHandler>();
        services.AddScoped<GetInstructorsQueryHandler>();
        services.AddScoped<AddInstructorCommandHandler>();
        services.AddScoped<ModifyInstructorsCommandHandler>();
        services.AddScoped<GetCoursesQueryHandler>();
        services.AddScoped<ModifyCoursesCommandHandler>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var tt = typeof(DivingApplication);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(tt.Assembly));
    }
}
