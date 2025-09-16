using Catalogo.Application.DTOs;
using Catalogo.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalogo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : Controller
{
    private readonly IProdutoService _produtoService;
    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetAsync()
    {
        try
        {
            var produtos = await _produtoService.GetProdutos();
            return Ok(produtos);
        }
        catch
        {
            throw;
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]    
    [HttpGet("{id:int}", Name = "GetProdutoPorId")]
    public async Task<ActionResult<ProdutoDTO>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido");

        var produto = await _produtoService.GetById(id); 

        if (produto is null)
            return NotFound("Produto não encontrado");

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> PostAsync(ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
            return BadRequest("Produto inválido");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var produtoCriado = await _produtoService.Create(produtoDTO);

        return new CreatedAtRouteResult("Post", new { id = produtoCriado.Id }, produtoCriado);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoDTO>> PutAsync(ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
            return BadRequest("Produto inválido");

        if (produtoDTO.Id <= 0)
            return BadRequest("É necessário informar o Id do produto");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var produtoAtualizada = await _produtoService.Update(produtoDTO);
        return Ok(produtoAtualizada);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("É necessário informar o Id do produto");

        var excluido = await _produtoService.Remove(id);

        if (excluido)
            return Ok(true);
        else
            return StatusCode((int)HttpStatusCode.InternalServerError, "Ocorreu um problema ao excluir o produto.");
    }
}
