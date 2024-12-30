using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace JiraAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private JiraContext _context;

    private bool CheckID(Guid guid) {
        return _context.Projects.Any(e => e.ProjectId == guid);
    }

    public ProjectsController(JiraContext context)
    {
        this._context = context;

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    [HttpGet("{Guid:guid}")]
    public async Task<ActionResult<Project>> GetProject(Guid guid)
    {
        var project = await _context.Projects.FindAsync(guid);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

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
        _context.Entry(project).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return NoContent();

    }

    [HttpDelete("{Guid:Guid}")]
    public async Task<ActionResult> DeleteProject(Guid guid) {
        var project = await _context.Projects.FindAsync(guid);
        if (project == null) {
            return NotFound();
        }
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
