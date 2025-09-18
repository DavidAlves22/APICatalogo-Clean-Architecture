namespace Catalogo.Domain.Entities;

public class Role
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string Name { get; private set; }

    public Role(string name)
    {
        Name = name;
    }
}
