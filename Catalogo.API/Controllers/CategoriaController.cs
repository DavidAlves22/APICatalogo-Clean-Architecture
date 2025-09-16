using Catalogo.Application.DTOs;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;
    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAsync()
    {
        try
        {
            var categorias = await _categoriaService.GetCategorias();
            return Ok(categorias);
        }
        catch
        {
            throw;
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetCategoriaPorId")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido");

        var categoria = await _categoriaService.GetById(id);

        if (categoria is null)
            return NotFound("Categoria não encontrada");

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> Post([FromBody] CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest("Categoria inválida");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var categoria = await _categoriaService.Create(categoriaDTO);

        return new CreatedAtRouteResult("Post", new { id = categoria.Id }, categoria);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<ActionResult<Categoria>> Put([FromBody] CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest("Categoria inválida");

        if (categoriaDTO.Id <= 0)
            return BadRequest("É necessário informar o Id da categoria");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoriaAtualizada = await _categoriaService.Update(categoriaDTO);
        return Ok(categoriaAtualizada);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("É necessário informar o Id da categoria");

        var excluido = await _categoriaService.Remove(id);

        if (excluido)
            return Ok(true);
        else
            return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um problema ao excluir a categoria.");
    }
}
