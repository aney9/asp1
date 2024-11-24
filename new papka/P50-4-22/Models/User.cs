using System;
using System.Collections.Generic;

namespace P50_4_22.Models;

public partial class User
{
    public int IdUsers { get; set; }

    public string Loginvhod { get; set; } = null!;

    public string Loginpassword { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
