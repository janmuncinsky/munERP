namespace MunCode.Core.Serializaton
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using EasyNetQ;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;

    public class ShortNameTypeNameSerializer : ITypeNameSerializer
    {
        private readonly IEnumerable<IConsumerDefinition> endpointDefinitions;
        private readonly ConcurrentDictionary<Type, string> serializedTypes = new ConcurrentDictionary<Type, string>();
        private readonly ConcurrentDictionary<string, Type> deserializedTypes = new ConcurrentDictionary<string, Type>();

        public ShortNameTypeNameSerializer(ICollection<IConsumerDefinition> endpointDefinitions)
        {
            Guard.NotNull(endpointDefinitions, nameof(endpointDefinitions));
            this.endpointDefinitions = endpointDefinitions;
        }

        public string Serialize(Type type)
        {
            return this.serializedTypes.GetOrAdd(
                type,
                t =>
                    {
                        string str = t.Name;
                        if (str.Length <= byte.MaxValue)
                        {
                            return str;
                        }

                        throw new EasyNetQException(
                            $"The serialized name of type '{t.Name}' exceeds the AMQP maximum short string length of 255 characters");
                    });
        }

        public Type DeSerialize(string typeName)
        {
            return this.deserializedTypes.GetOrAdd(
                typeName,
                tn =>
                    {
                        var types = this.endpointDefinitions.Select(ed => ed.ContainsType(typeName))
                            .Where(t => t != null)
                            .Distinct()
                            .ToList();

                        if (types.Count > 1)
                        {
                            throw new EasyNetQException($"Multiple types named '{tn}' found.");
                        }

                        if (types.Count == 0)
                        {
                            throw new EasyNetQException($"Could not find any type named '{tn}'.");
                        }

                        return types.Single();
                    });
        }
    }
}