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

  public Task<IReadOnlyList<string>> GetProjectCategoriesAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<IReadOnlyList<Project>> GetProjectsAsync()
    {
        return await context.Projects.ToListAsync();
    }

  public Task<IReadOnlyList<Project>> GetProjectsByArchivedAsync(bool archived)
  {
    throw new NotImplementedException();
  }

  public Task<IReadOnlyList<Project>> GetProjectsByCategoryAsync(string category)
  {
    throw new NotImplementedException();
  }

  public Task<IReadOnlyList<Project>> GetProjectsByCreatedDateAsync(DateTime createdDate)
  {
    throw new NotImplementedException();
  }

  public Task<IReadOnlyList<Project>> GetProjectsByDeadlineAsync(DateTime deadline)
  {
    throw new NotImplementedException();
  }

  public Task<IReadOnlyList<Project>> GetProjectsByLastModifiedDateAsync(DateTime lastModifiedDate)
  {
    throw new NotImplementedException();
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
