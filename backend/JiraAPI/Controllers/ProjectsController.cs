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
}
