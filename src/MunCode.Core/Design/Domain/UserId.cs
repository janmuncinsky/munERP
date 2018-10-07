namespace MunCode.Core.Design.Domain
{
    public class UserId : ValueObject<UserId>
    {
        private UserId()
        {
        }

        public string UserName { get; private set; }

        public static UserId GetCurrentUser()
        {
            // todo implement IMessagePipelineFilter setting user context
            return new UserId { UserName = "TestUser" /*Thread.CurrentPrincipal.Identity.Name*/ };
        }
    }
}