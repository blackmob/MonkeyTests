namespace GreaterHeights.Interfaces
{
    using System.Data.Entity;

    using GreaterHeights.Domain;

    public interface IMonkeyContext 
    {
        IDbSet<AccidentReport> Reports { get; set; }

        int Save();
    }
}