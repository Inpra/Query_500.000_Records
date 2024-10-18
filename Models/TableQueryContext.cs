using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Data_query.Models;

public partial class TableQueryContext : DbContext
{
    public TableQueryContext()
    {
    }

    public TableQueryContext(DbContextOptions<TableQueryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<EmployeeEventStep> EmployeeEventSteps { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Input> Inputs { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sheet> Sheets { get; set; }

    public virtual DbSet<SsoSession> SsoSessions { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<StepResult> StepResults { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("TableQueryContext");
        optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Domain, "domain").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EmployeeEventStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee_event_steps");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.HasIndex(e => e.EventId, "event_id");

            entity.HasIndex(e => e.ManagerId, "manager_id");

            entity.HasIndex(e => e.StepId, "step_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("assigned_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.IsCompleted)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_completed");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.StepId).HasColumnName("step_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeEventStepEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("employee_event_steps_ibfk_1");

            entity.HasOne(d => d.Event).WithMany(p => p.EmployeeEventSteps)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("employee_event_steps_ibfk_2");

            entity.HasOne(d => d.Manager).WithMany(p => p.EmployeeEventStepManagers)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("employee_event_steps_ibfk_4");

            entity.HasOne(d => d.Step).WithMany(p => p.EmployeeEventSteps)
                .HasForeignKey(d => d.StepId)
                .HasConstraintName("employee_event_steps_ibfk_3");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.CompanyId, "company_id");

            entity.HasIndex(e => e.CreatedBy, "created_by");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Company).WithMany(p => p.Events)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("events_ibfk_1");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("events_ibfk_2");
        });

        modelBuilder.Entity<Input>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inputs");

            entity.HasIndex(e => e.SheetId, "sheet_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HtmlElement)
                .HasColumnType("text")
                .HasColumnName("html_element");
            entity.Property(e => e.InputLabel)
                .HasMaxLength(255)
                .HasColumnName("input_label");
            entity.Property(e => e.InputName)
                .HasMaxLength(255)
                .HasColumnName("input_name");
            entity.Property(e => e.InputType)
                .HasColumnType("enum('text','number','date','textarea','checkbox','radio','select')")
                .HasColumnName("input_type");
            entity.Property(e => e.IsEditable)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_editable");
            entity.Property(e => e.IsRequired)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_required");
            entity.Property(e => e.IsVisible)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_visible");
            entity.Property(e => e.Options)
                .HasColumnType("json")
                .HasColumnName("options");
            entity.Property(e => e.SheetId).HasColumnName("sheet_id");

            entity.HasOne(d => d.Sheet).WithMany(p => p.Inputs)
                .HasForeignKey(d => d.SheetId)
                .HasConstraintName("inputs_ibfk_1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.InputId, "input_id");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CanEdit)
                .HasDefaultValueSql("'0'")
                .HasColumnName("can_edit");
            entity.Property(e => e.CanView)
                .HasDefaultValueSql("'1'")
                .HasColumnName("can_view");
            entity.Property(e => e.InputId).HasColumnName("input_id");
            entity.Property(e => e.IsHidden)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_hidden");
            entity.Property(e => e.IsRequired)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_required");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Input).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.InputId)
                .HasConstraintName("permissions_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("permissions_ibfk_2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Sheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sheets");

            entity.HasIndex(e => e.EventId, "event_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.HtmlContent).HasColumnName("html_content");
            entity.Property(e => e.SheetName)
                .HasMaxLength(255)
                .HasColumnName("sheet_name");

            entity.HasOne(d => d.Event).WithMany(p => p.Sheets)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("sheets_ibfk_1");
        });

        modelBuilder.Entity<SsoSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sso_sessions");

            entity.HasIndex(e => e.CompanyId, "company_id");

            entity.HasIndex(e => e.SsoToken, "sso_token").IsUnique();

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("login_time");
            entity.Property(e => e.SsoToken).HasColumnName("sso_token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.SsoSessions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sso_sessions_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.SsoSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sso_sessions_ibfk_1");
        });

        modelBuilder.Entity<Step>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("steps");

            entity.HasIndex(e => e.CreatedBy, "created_by");

            entity.HasIndex(e => e.EventId, "event_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.StepName)
                .HasMaxLength(255)
                .HasColumnName("step_name");
            entity.Property(e => e.StepOrder).HasColumnName("step_order");
            entity.Property(e => e.StepType)
                .HasColumnType("enum('Employee','Manager')")
                .HasColumnName("step_type");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Steps)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("steps_ibfk_2");

            entity.HasOne(d => d.Event).WithMany(p => p.Steps)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("steps_ibfk_1");
        });

        modelBuilder.Entity<StepResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("step_results");

            entity.HasIndex(e => e.EmployeeEventStepId, "employee_event_step_id");

            entity.HasIndex(e => e.EvaluatorId, "evaluator_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeEventStepId).HasColumnName("employee_event_step_id");
            entity.Property(e => e.EvaluatorId).HasColumnName("evaluator_id");
            entity.Property(e => e.InputData)
                .HasColumnType("json")
                .HasColumnName("input_data");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'In Progress'")
                .HasColumnType("enum('Pending','In Progress','Completed')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.EmployeeEventStep).WithMany(p => p.StepResults)
                .HasForeignKey(d => d.EmployeeEventStepId)
                .HasConstraintName("step_results_ibfk_1");

            entity.HasOne(d => d.Evaluator).WithMany(p => p.StepResults)
                .HasForeignKey(d => d.EvaluatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("step_results_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.CompanyId, "company_id");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Users)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("users_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
