using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Key).IsRequired().HasMaxLength(10);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.CreatedDate).IsRequired();
        builder.Property(p => p.LastModifiedDate);
        builder.Property(p => p.Deadline);
        builder.Property(p => p.OwnerId).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.Progress).IsRequired();
        builder.Property(p => p.Priority).IsRequired();
        builder.Property(p => p.Category).HasMaxLength(50);
        builder.Property(p => p.Archived).IsRequired();

        // projectId: Unique identifier for the project (must be a GUID).
        // name: The project name (required, max length 50).
        // key: Short project key (required, max length 10).
        // description: Detailed description (required, max length 500).
        // createdDate: The date the project was created.
        // lastModifiedDate: The last date the project was modified (nullable).
        // deadline: Project deadline (nullable).
        // ownerId: GUID of the project owner (should match a user in the database).
        // status: Enum for project status (e.g., 0 = New, 1 = In Progress, 2 = Completed).
        // progress: Float for project progress as a percentage (0 to 100).
        // priority: Enum for project priority (1 = Critical, 2 = High, 3 = Medium).
        // category: A string categorizing the project.
        // archived: Boolean indicating if the project is archived.
    }
}
