using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyBrandNewCV.Api.Services;
using MyBrandNewCV.Api.Services.Interfaces;
using MyBrandNewCv.Common.Models;
using MyBrandNewCV.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// 
app.MapGet("/api/skills", async (ISkillRepository skillRepository) =>
{
    try
    {
        var skills = await skillRepository.GetAllAsync();
        return Results.Ok(skills);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex);
        return Results.NotFound("Internal Server Error");
    }
});

app.MapGet("/api/projects", async (IProjectRepository projectRepository) =>
{
    try
    {
        var projects = await projectRepository.GetAllAsync();
        return Results.Ok(projects);

    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex);
        return Results.NotFound("Internal Server Error");
    }
});

app.MapPost("/api/skills", async (ISkillRepository skillRepository, Skill newSkill) =>
{
    try
    {
        Console.WriteLine(newSkill + "test");
       
        if (string.IsNullOrWhiteSpace(newSkill.Name) || newSkill.YearsOfExperience <= 0 || string.IsNullOrWhiteSpace(newSkill.SkillLevel))
        {
            return Results.BadRequest("Invalid skill data");
        }

        
        var userId = newSkill.UserId;

       
        newSkill.UserId = userId;

      
        await skillRepository.AddAsync(newSkill);

       
        return Results.Created($"/api/skills/{newSkill.Id}", newSkill);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex);
        return Results.NotFound("Internal Server Error");
    }
});


app.MapPost("api/projects", async (IProjectRepository projectRepository, Project newProject) =>
{
    try
    {
        if (string.IsNullOrWhiteSpace(newProject.Name) || string.IsNullOrWhiteSpace(newProject.Description))
        {
            return Results.BadRequest("Invalid project data");

        }

        var userId = newProject.UserId;
        newProject.UserId = userId;
        await projectRepository.AddAsync(newProject);
        return Results.Created($"/api/projects/{newProject.Id}", newProject);

    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex);
        return Results.NotFound("Internal Server Error");
    }
});


app.MapPut("/api/skills/{id}", async (ISkillRepository skillRepository, int id, Skill updatedSkill) =>
{
    try
    {
        var skillToUpdate = await skillRepository.GetByIdAsync(id);

        if (skillToUpdate == null)
        {
            return Results.NotFound($"Skill with ID {id} not found");
        }

       
        skillToUpdate.Name = updatedSkill.Name;
        skillToUpdate.YearsOfExperience = updatedSkill.YearsOfExperience;
        skillToUpdate.SkillLevel = updatedSkill.SkillLevel;
        skillToUpdate.UserId=updatedSkill.UserId;

        
        await skillRepository.UpdateAsync(skillToUpdate);

        return Results.Ok("Skill updated");
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error updating skill: {ex.Message}");
        return Results.NotFound("Internal Server Error");
    }
});

app.MapPut("/api/projects/{id}", async (IProjectRepository projectRepository, int id, Project updatedProject) =>
{
    try
    {
        var projectToUpdate = await projectRepository.GetByIdAsync(id);
        if (projectToUpdate == null)
        {
            return Results.NotFound($"Project with ID {id} not found");

        }

        projectToUpdate.Name = updatedProject.Name;
        projectToUpdate.Description = updatedProject.Description;
        projectToUpdate.UserId = updatedProject.UserId;

        await projectRepository.UpdateAsync(projectToUpdate);
        return Results.Ok("Project updated");
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error updating project: {ex.Message}");
        return Results.NotFound("Internal Server Error");
    }
});

app.MapDelete("/api/skills/{id}", async (ISkillRepository skillRepository, int id) =>
    {
        try
        {
            var skillToDelete = await skillRepository.GetByIdAsync(id);

            if (skillToDelete == null)
            {
                return Results.NotFound($"Skill with ID {id} not found");
            }

            
            await skillRepository.DeleteAsync(id);

            return Results.Ok("Skill deleted");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error deleting skill: {ex.Message}");
            return Results.NotFound("Internal Server Error");
        }
    });

app.MapDelete("/api/projects/{id}", async (IProjectRepository projectRepository , int id)=>

{
    try
    {
        var projectToDelete = await projectRepository.GetByIdAsync(id);
        if (projectToDelete == null)
        {
            return Results.NotFound($"Project with ID {id} not found");
        }

        await projectRepository.DeleteAsync(id);
        return Results.Ok("Project deleted");
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"Error deleting project: {ex.Message}");
        return Results.NotFound("Internal Server Error");
    }
});

app.Run();


