using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProjectRepository
{
    Task<IReadOnlyList<Project>> GetProjectsAsync();
    Task<Project?> GetProjectByIdAsync(Guid id);
    void PostProject(Project project);
    void UpdateProject(Project project);
    void DeleteProject(Project project);
    bool CheckID(Guid guid);
    Task<bool> SaveAllAsync();
    Task PostMultipleProjectsAsync(IEnumerable<Project> projects);
    Task<IReadOnlyList<string>> GetProjectCategoriesAsync();
    Task<IReadOnlyList<Project>> GetProjectsByCategoryAsync(string category);
    Task<IReadOnlyList<Project>> GetProjectsByArchivedAsync(bool archived);
    Task<IReadOnlyList<Project>> GetProjectsByCreatedDateAsync(DateTime createdDate);
    Task<IReadOnlyList<Project>> GetProjectsByDeadlineAsync(DateTime deadline);
    Task<IReadOnlyList<Project>> GetProjectsByLastModifiedDateAsync(DateTime lastModifiedDate);
    

}
