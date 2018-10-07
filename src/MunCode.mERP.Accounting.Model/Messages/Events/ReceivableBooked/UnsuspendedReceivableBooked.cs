﻿namespace MunCode.mERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    public class UnsuspendedReceivableBooked : ReceivableBooked
    {
        public UnsuspendedReceivableBooked(Guid orderId)
            : base(orderId)
        {
        }
    }
}