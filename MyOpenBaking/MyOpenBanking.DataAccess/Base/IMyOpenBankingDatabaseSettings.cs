namespace MyOpenBanking.DataAccess.Base
{
    public interface IMyOpenBankingDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
