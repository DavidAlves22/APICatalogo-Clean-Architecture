using Catalogo.Application.DTOs;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using MapsterMapper;

namespace Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProdutoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
    {
        var produtos = await _unitOfWork.ProdutoRepository.GetAsync();
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return produtosDTO;
    }

    public async Task<ProdutoDTO> GetById(int id)
    {
        var produto = await _unitOfWork.ProdutoRepository.GetByIdAsync(id);
        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
        return produtoDTO;
    }

    public async Task<ProdutoDTO> Create(ProdutoDTO produtoDTO)
    {
        var produto = _mapper.Map<Produto>(produtoDTO);
        produto.DataCadastro = DateTime.UtcNow;
        var produtoCriado = _unitOfWork.ProdutoRepository.Create(produto);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProdutoDTO>(produtoCriado);
    }

    public async Task<bool> Remove(int id)
    { 
        _unitOfWork.ProdutoRepository.Remove(id);
        var retorno = await _unitOfWork.CommitAsync();

        return retorno > 0;
    }

    public async Task<ProdutoDTO> Update(ProdutoDTO produtoDTO)
    {
        var produto = _mapper.Map<Produto>(produtoDTO);
        var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ProdutoDTO>(produtoAtualizado);
    }
}
