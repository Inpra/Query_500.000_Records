using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class SsoSession
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CompanyId { get; set; }

    public string? SsoToken { get; set; }

    public DateTime? LoginTime { get; set; }

    public virtual Company? Company { get; set; }

    public virtual User? User { get; set; }
}
