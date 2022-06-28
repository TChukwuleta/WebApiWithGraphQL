using APIwithGraphQL.Data;
using APIwithGraphQL.Models;

namespace APIwithGraphQL.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Represents any software or service that has a command line interface");
            descriptor.Field(c => c.LicenseKey).Ignore();
            descriptor.Field(c => c.Commands)
                .ResolveWith<Resolvers>(c => c.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("This is the list of availale commands for this platform");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands(Platform platform, [ScopedService] AppDbContext context)
            {
                return context.Commands.Where(c => c.PlatformId == platform.Id);
            }
        }
    }
}
