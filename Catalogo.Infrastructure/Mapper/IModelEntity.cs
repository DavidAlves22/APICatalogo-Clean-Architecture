namespace Catalogo.Infrastructure.Mapper;

public interface IModelEntity<E, M>
{
    E ToDomain(M model);

    M ToModel(E entidade);
}
