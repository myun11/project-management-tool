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
}
