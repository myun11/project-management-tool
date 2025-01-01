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

  public async Task<IReadOnlyList<string>> GetProjectCategoriesAsync()
  {
    return await context.Projects.Select(p => p.Category).Distinct().ToListAsync();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsAsync()
  {
    return await context.Projects.ToListAsync();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsByArchivedAsync(bool archived)
  {
    return await context.Projects.Where(p => p.Archived == archived).ToListAsync();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsByCategoryAsync(string category)
  {
    return await context.Projects.Where(p => p.Category == category).ToListAsync();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsByCreatedDateAsync(DateTime createdDate)
  {
    return await context.Projects.Where(p => p.CreatedDate >= createdDate).ToListAsync();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsByDeadlineAsync(DateTime deadline)
  {
    return await context.Projects.Where(p => p.Deadline <= deadline).ToListAsync();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsByLastModifiedDateAsync(DateTime lastModifiedDate)
  {
    return await context.Projects.Where(p => p.LastModifiedDate >= lastModifiedDate).ToListAsync();
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
