using System;

namespace Core.Entities;

public class Project : BaseEntity
{

    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public DateTime? Deadline { get; set; }
    public Guid OwnerId { get; set; }
    // public ICollection<User> Members { get; set; }
    // public ICollection<Task> Tasks { get; set; }
    // public ICollection<Comment> Comments { get; set; }
    // public ICollection<Attachment> Attachments { get; set; }
    // public ICollection<AuditLog> AuditLog { get; set; }
    public ProjectStatus Status { get; set; }
    public float Progress { get; set; }
    public ProjectPriority Priority { get; set; }
    public string Category { get; set; }
    public bool Archived { get; set; }
}

public enum ProjectStatus
{
    New,
    InProgress,
    Completed,
    OnHold
}

public enum ProjectPriority
{
    Low,
    Medium,
    High,
    Critical
}
