namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Collections.Generic;
    using System.Reflection;

    using Microsoft.AspNetCore.Mvc.ApplicationParts;

    using MunCode.Core.Guards;

    public class InputMessageChannelApplicationPart : ApplicationPart, IApplicationPartTypeProvider
    {
        private readonly IList<TypeInfo> types = new List<TypeInfo>();

        public override string Name => "InputMessageChannelController";

        public IEnumerable<TypeInfo> Types => this.types;

        public void AddType(TypeInfo type)
        {
            Guard.NotNull(type, nameof(type));
            this.types.Add(type);
        }
    }
}