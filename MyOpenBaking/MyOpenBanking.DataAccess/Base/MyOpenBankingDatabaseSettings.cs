namespace MyOpenBanking.DataAccess.Base
{
    public class MyOpenBankingDatabaseSettings : IMyOpenBankingDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
