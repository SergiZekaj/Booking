using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Domain.Enums
{
    public enum VerificationStatus
    {
        Pending,
        Approved,
        Rejected,
        Cancelled,
        Expired,
        Completed
    }
}
