using Membership.Application.Handlers.Members.Commands.Create;
using Membership.Application.Handlers.Members.Commands.Delete;
using Membership.Application.Handlers.Members.Commands.Update;
using Membership.Application.Handlers.Members.Queries.GetById;
using Membership.Application.Handlers.Members.Queries.GetPaged;
using Microsoft.Extensions.DependencyInjection;

namespace Membership.Application.Handlers.Members;

public static class IncludeMemberHandlers
{
    public static void Include(IServiceCollection builder)
    {
        builder.AddScoped<CreateMemberHandler>();
        builder.AddScoped<UpdateMemberHandler>();
        builder.AddScoped<DeleteMemberHandler>();
        builder.AddScoped<GetMemberByIdHandler>();
        builder.AddScoped<GetMembersPagedHandler>();
    }
}