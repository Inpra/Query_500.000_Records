using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class Permission
{
    public int Id { get; set; }

    public int InputId { get; set; }

    public int RoleId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanEdit { get; set; }

    public bool? IsRequired { get; set; }

    public bool? IsHidden { get; set; }

    public virtual Input Input { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
