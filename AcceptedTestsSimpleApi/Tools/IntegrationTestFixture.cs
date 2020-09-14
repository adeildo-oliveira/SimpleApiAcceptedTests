using Xunit;

namespace AcceptedTestsSimpleApi.Tools
{
    [Collection(Name)]
    public class IntegrationTestFixture : IClassFixture<DatabaseFixture>
    {
        private const string Name = nameof(IntegrationTestFixture);
        private readonly DatabaseFixture _fixture;

        public IntegrationTestFixture(DatabaseFixture fixture) => _fixture = fixture;
    }
}
