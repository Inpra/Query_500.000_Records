using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class StepResult
{
    public int Id { get; set; }

    public int EmployeeEventStepId { get; set; }

    public int EvaluatorId { get; set; }

    public string InputData { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual EmployeeEventStep EmployeeEventStep { get; set; } = null!;

    public virtual User Evaluator { get; set; } = null!;
}
