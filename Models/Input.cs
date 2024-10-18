using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class Input
{
    public int Id { get; set; }

    public int SheetId { get; set; }

    public string InputType { get; set; } = null!;

    public string? InputName { get; set; }

    public string? InputLabel { get; set; }

    public string? HtmlElement { get; set; }

    public bool? IsRequired { get; set; }

    public bool? IsEditable { get; set; }

    public bool? IsVisible { get; set; }

    public string? Options { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual Sheet Sheet { get; set; } = null!;
}
