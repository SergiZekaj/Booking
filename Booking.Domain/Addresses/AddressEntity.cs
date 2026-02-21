using Booking.Domain.Estate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking.Domain.Addresses
{
    public class AddressEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public ICollection<PropertyEntity> Properties { get; set; } = new List<PropertyEntity>();
    }
}
