namespace Database.Seed
{
    public interface ISeedSettings
    {
        string ClientDataRelativePath { get; }
        string RentsDataRelativePath { get; }
        string CarsDataRelativePath { get; }
    }
}