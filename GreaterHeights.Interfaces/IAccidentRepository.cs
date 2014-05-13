using GreaterHeights.Domain;

namespace GreaterHeights.Interfaces
{
    public interface IAccidentRepository
    {
        bool Authorised { get; }

        AccidentReport ReportAccident(AccidentReport report);
    }
}