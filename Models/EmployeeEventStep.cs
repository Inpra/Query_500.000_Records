using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class EmployeeEventStep
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int EventId { get; set; }

    public int StepId { get; set; }

    public int? ManagerId { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual User Employee { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User? Manager { get; set; }

    public virtual Step Step { get; set; } = null!;

    public virtual ICollection<StepResult> StepResults { get; set; } = new List<StepResult>();
}
