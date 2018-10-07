namespace MunCode.Core.Guards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Guard
    {
        public static void NotNull<T>(T param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void NotNull<T>(T param, Exception ex)
        {
            if (param == null)
            {
                throw ex;
            }
        }

        public static void NotEmpty<T>(IEnumerable<T> collection, string collectionName)
        {
            if (!collection.Any())
            {
               throw new ArgumentException($"Collection '{collectionName}' is empty.");
            }
        }

        public static void MustBeOfType<TInput, TOutput>(TInput param, string paramName)
        {
            if (!(param is TOutput))
            {
                throw new ArgumentException($"Parameter '{paramName}' must be of type '{typeof(TOutput)}'.");
            }
        }

        public static void NotNegative<T>(T value, string valueName)
            where T : IComparable<T> 
        {
            if (value.CompareTo(default(T)) <= 0)
            {
                throw new ArgumentOutOfRangeException($"Value '{valueName}' cannot be negative.");
            }
        }
    }
}