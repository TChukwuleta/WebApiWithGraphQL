using APIwithGraphQL.Models;

namespace APIwithGraphQL.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
        {
            return platform;
        }
    }
}
