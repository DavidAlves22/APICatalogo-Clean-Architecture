using Testcontainers.MySql;

namespace Catalogo.IntegrationTests.Fixtures
{
    public class MySqlFixture : IAsyncLifetime
    {
        public MySqlContainer Container { get; private set; } = default!;

        public string ConnectionString => Container.GetConnectionString();

        public async Task InitializeAsync()
        {
            Container = new MySqlBuilder()
                .WithImage("mysql:8.0")
                .WithDatabase("apiCatalogo")
                .WithUsername("david")
                .WithPassword("@David123")
                .Build();

            await Container.StartAsync();
        }

        public async Task DisposeAsync()
        {
            if (Container != null)
                await Container.DisposeAsync();
        }
    }
}
