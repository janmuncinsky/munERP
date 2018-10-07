namespace MunCode.Core.Design.Domain
{
    using System;
    using System.Collections.Generic;

    public class AuditTrail : ValueObject<AuditTrail>
    {
        private static DateTime? now;
        private DateTime created;
        private string createdBy;

        public static Comparer<AuditTrail> ByCreatedComparerInstance { get; } = new ByCreatedComparer();

        public static AuditTrail GetCurrent()
        {
            var auditTrail = new AuditTrail
                                 {
                                     createdBy = UserId.GetCurrentUser().UserName,
                                     created = now ?? DateTime.Now
                                 };
            return auditTrail;
        }

        internal static void ConfigureNow(DateTime dateTime)
        {
            now = dateTime;
        }

        private sealed class ByCreatedComparer : Comparer<AuditTrail>
        {
            public override int Compare(AuditTrail x, AuditTrail y)
            {
                if (ReferenceEquals(null, y))
                {
                    return 1;
                }

                if (ReferenceEquals(null, x))
                {
                    return -1;
                }

                if (x.Equals(y))
                {
                    return 0;
                }

                return x.created.CompareTo(y.created);
            }
        }
    }
}