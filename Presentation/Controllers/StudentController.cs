using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GetStudentRequest = Application.UseCases.Student.GetById.Request;
using GetAllStudentsRequest = Application.UseCases.Student.GetAll.Request;
using CreateStudentRequest = Application.UseCases.Student.Create.Request;
using DeleteStudentRequest = Application.UseCases.Student.Delete.Request;

namespace Presentation.Controllers;

/// <summary>
/// Controller responsável pelos métodos de retorno, exclução e criação de Estudantes.
/// </summary>
[ApiController]
[Route("Student")]
[Authorize]
public class StudentController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Método responsável por retornar um estudante do sistema pelo seu identificador.
    /// </summary>
    /// <param name="id">Identificador para o filtro</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns><see cref="IActionResult"/> com status e objeto encontrado</returns>
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new GetStudentRequest( StudentId: id ), cancellationToken);
            return StatusCode(response.statuscode, new {response.message, response.Response});
        }
        catch(Exception e)
        {   
            return StatusCode(500, e.StackTrace);
        }
    }

    /// <summary>
    /// Método responsável por retornar até 100 estudantes do sistema.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns><see cref="IActionResult"/> com status e objeto encontrado</returns>
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new GetAllStudentsRequest(), cancellationToken);
            return StatusCode(response.statuscode, new {response.message, response.Response});
        }
        catch(Exception e)
        {   
            return StatusCode(500, e.StackTrace);
        }
    }

    /// <summary>
    /// Método responsável por criar um estudante no sistema.
    /// </summary>
    /// <param name="request">Objeto com os parâmetros necessários para a criação de um estudante</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns><see cref="IActionResult"/> com status e objeto encontrado</returns>
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateStudentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(response.statuscode, new {response.message, response.Response});
        }
        catch(Exception e)
        {   
            return StatusCode(500, e.StackTrace);
        }
    }

    /// <summary>
    /// Método responsável por deletar um estudante no sistema.
    /// </summary>
    /// <param name="id">Identificador para a exclução</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns><see cref="IActionResult"/> com status e objeto encontrado</returns>
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await mediator.Send(new DeleteStudentRequest( StudentId: id ), cancellationToken);
            return StatusCode(response.statuscode, new {response.message, response.Response});
        }
        catch(Exception e)
        {   
            return StatusCode(500, e.StackTrace);
        }
    } 
}
