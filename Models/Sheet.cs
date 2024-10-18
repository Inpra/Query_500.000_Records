using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class Sheet
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public string SheetName { get; set; } = null!;

    public string HtmlContent { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Input> Inputs { get; set; } = new List<Input>();
}
