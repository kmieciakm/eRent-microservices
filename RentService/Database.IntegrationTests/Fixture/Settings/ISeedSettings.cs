namespace Database.IntegrationTests.Fixture.Settings
{
    public interface ISeedSettings
    {
        string ClientDataRelativePath { get; }
        string RentsDataRelativePath { get; }
    }
}