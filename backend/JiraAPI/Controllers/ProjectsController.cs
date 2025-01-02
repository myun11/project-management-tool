using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Core.Interfaces;

namespace JiraAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectRepository repository) : ControllerBase
{


    private bool CheckID(Guid guid) {
        return repository.CheckID(guid);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjects()
    {
        var projects = await repository.GetProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("{Guid:guid}")]
    public async Task<ActionResult<Project>> GetProject(Guid guid)
    {
        var project = await repository.GetProjectByIdAsync(guid);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
        repository.PostProject(project);
        await repository.SaveAllAsync();
        return project;
        // return CreatedAtAction("GetProject", new { id = project.Id }, project);
    }

    [HttpPost("multiple")]
    public async Task<ActionResult> PostMultipleProjects(IEnumerable<Project> projects)
    {
        await repository.PostMultipleProjectsAsync(projects);
        await repository.SaveAllAsync();
        return NoContent();
    }

    [HttpPut("{Guid:guid}")]
    public async Task<ActionResult> UpdateProject(Guid guid, Project project) {
        if (!CheckID(project.ProjectId)) {
            return BadRequest("Invalid project ID while updating project. ID does not exist.");
        }
        if (project.ProjectId != guid) {
            return BadRequest("Invalid project ID while updating project. ID does not match.");
        }
        repository.UpdateProject(project);
        await repository.SaveAllAsync();
        return NoContent();

    }

    [HttpDelete("{Guid:Guid}")]
    public async Task<ActionResult> DeleteProject(Guid guid) {
        var project = await repository.GetProjectByIdAsync(guid);
        if (project == null) {
            return NotFound();
        }
        repository.DeleteProject(project);
        await repository.SaveAllAsync();
        return NoContent();
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProjectCategories() {
        var categories = await repository.GetProjectCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjectsByCategory(string category) {
        var projects = await repository.GetProjectsByCategoryAsync(category);
        return Ok(projects);
    }

    [HttpGet("archived/{archived}")]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjectsByArchived(bool archived) {
        var projects = await repository.GetProjectsByArchivedAsync(archived);
        return Ok(projects);
    }

    [HttpGet("created/{createdDate}")]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjectsByCreatedDate(DateTime createdDate) {
        var projects = await repository.GetProjectsByCreatedDateAsync(createdDate);
        return Ok(projects);
    }

    [HttpGet("deadline/{deadline}")]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjectsByDeadline(DateTime deadline) {
        var projects = await repository.GetProjectsByDeadlineAsync(deadline);
        return Ok(projects);
    }

    [HttpGet("lastmodified/{lastModifiedDate}")]
    public async Task<ActionResult<IReadOnlyList<Project>>> GetProjectsByLastModifiedDate(DateTime lastModifiedDate) {
        var projects = await repository.GetProjectsByLastModifiedDateAsync(lastModifiedDate);
        return Ok(projects);
    }
}
