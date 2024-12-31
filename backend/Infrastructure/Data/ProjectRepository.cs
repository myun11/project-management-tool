using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProjectRepository(JiraContext context) : IProjectRepository
{   

    public bool CheckID(Guid guid)
    {
        return context.Projects.Any(e => e.ProjectId == guid);
    }

    public void DeleteProject(Project project)
    {
        context.Projects.Remove(project);
    }

    public async Task<Project?> GetProjectByIdAsync(Guid id)
    {
        return await context.Projects.FindAsync(id);
    }

    public async Task<IReadOnlyList<Project>> GetProjectsAsync()
    {
        return await context.Projects.ToListAsync();
    }

    public async Task PostMultipleProjectsAsync(IEnumerable<Project> projects)
    {
        await context.Projects.AddRangeAsync(projects);
    }

    public void PostProject(Project project)
    {
        context.Projects.Add(project);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProject(Project project)
    {
        context.Entry(project).State = EntityState.Modified;
    }
}
