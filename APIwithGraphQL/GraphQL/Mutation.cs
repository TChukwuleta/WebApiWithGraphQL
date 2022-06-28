using APIwithGraphQL.Data;
using APIwithGraphQL.GraphQL.Commands;
using APIwithGraphQL.GraphQL.Platforms;
using APIwithGraphQL.Models;
using HotChocolate.Subscriptions;

namespace APIwithGraphQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input, 
            [ScopedService] AppDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var platform = new Platform
            {
                Name = input.Name,
                LicenseKey = input.LicenceKey
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);
            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.commadLine,
                PlatformId = input.platformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();
            return new AddCommandPayload(command);
        }
    }
}
