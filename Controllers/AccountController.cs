using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blog.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost("v1/accounts")]
    public async Task<IActionResult> Post([FromBody] RegisterAccountViewModel viewModel,
                                          [FromServices] EmailService emailService,
                                          [FromServices] AppDataContext context)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        User user = new()
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            Slug = viewModel.Email.Replace("@", "-").Replace(".", "-"),
        };

        string password = PasswordGenerator.Generate(length: 25, includeSpecialChars: true, upperCase: false);
        user.PasswordHash = PasswordHasher.Hash(password);

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            emailService.Send(user.Name,
                              user.Email,
                              subject: "Bem vindo ao blog!",
                              body: $"Sua senha é <strong>{password}</strong>");  

            return Ok(new ResultViewModel<dynamic>(data: new
            {
                user = user.Email,
                password
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, value: new ResultViewModel<string>(error: "05x99 - Este e-mail já está cadastrado."));
        }
        catch
        {
            return StatusCode(500, value: new ResultViewModel<string>(error: "Falha interna do servidor."));
        }

    }

    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel viewModel,
                                           [FromServices] AppDataContext context,
                                           [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(error: new ResultViewModel<string>(ModelState.GetErrors()));

        User? user = await context.Users
                                .AsNoTracking()
                                .Include(x => x.Roles)
                                .FirstOrDefaultAsync(x => x.Email == viewModel.Email);

        if (user is null)
            return StatusCode(401, new ResultViewModel<string>(error: "Usuário não existe."));

        bool verificado = PasswordHasher.Verify(user.PasswordHash, viewModel.Password);
        if (!verificado)
            return StatusCode(401, new ResultViewModel<string>(error: "Senha inválida."));

        try
        {
            string token = tokenService.GenerateToken(user);
            return Ok(new ResultViewModel<string>(token, errors:null));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>(error: "Falha interna no servidor."));
        }
    }

    [Authorize]
    [HttpPost("v1/accounts/upload-image")]
    public async Task<IActionResult> UploadImage([FromBody] UploadImageViewModel viewModel,
                                                 [FromServices] AppDataContext context)
    {
        string fileName = $"{Guid.NewGuid().ToString()}.jpg";
        string data = new Regex(pattern: @"^data:image \/ [a-z]+;base64,")
            .Replace(input: viewModel.Base64Image, replacement:"");
        byte[] bytes = Convert.FromBase64String(data);

        try
        {
            await System.IO.File.WriteAllBytesAsync(path: $"wwwroot/images/{fileName}", bytes);
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>(error: "05x04 - Falha interna do servidor."));
        }

        User? user = await context
            .Users
            .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

        if (user is null)
            return NotFound(new ResultViewModel<User>(error: "Usuário não encotrado."));

        user.Image = $"https://localhost:7238/images/{fileName}";
        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>(error: "05x04 - Falha interna do servidor."));
        }

        return Ok(new ResultViewModel<string>("Imagem alterada com sucesso!", errors: null));
    }
}
