using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class Step
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int StepOrder { get; set; }

    public string StepType { get; set; } = null!;

    public string StepName { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<EmployeeEventStep> EmployeeEventSteps { get; set; } = new List<EmployeeEventStep>();

    public virtual Event Event { get; set; } = null!;
}
