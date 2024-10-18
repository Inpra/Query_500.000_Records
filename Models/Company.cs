using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<SsoSession> SsoSessions { get; set; } = new List<SsoSession>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
