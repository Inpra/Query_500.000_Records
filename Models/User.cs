using System;
using System.Collections.Generic;

namespace Data_query.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int RoleId { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<EmployeeEventStep> EmployeeEventStepEmployees { get; set; } = new List<EmployeeEventStep>();

    public virtual ICollection<EmployeeEventStep> EmployeeEventStepManagers { get; set; } = new List<EmployeeEventStep>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SsoSession> SsoSessions { get; set; } = new List<SsoSession>();

    public virtual ICollection<StepResult> StepResults { get; set; } = new List<StepResult>();

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
}
