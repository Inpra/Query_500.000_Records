using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string? Category { get; set; }

    public int? CompanyId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company? Company { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<EmployeeEventStep> EmployeeEventSteps { get; set; } = new List<EmployeeEventStep>();

    public virtual ICollection<Sheet> Sheets { get; set; } = new List<Sheet>();

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
}
