using Booking.Domain.PropertyImage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Application.Abstractions.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
