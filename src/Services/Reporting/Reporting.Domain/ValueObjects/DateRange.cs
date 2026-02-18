
namespace Reporting.Domain.ValueObjects;

public class DateRange
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public DateRange(DateTime start, DateTime end)
    {
        if (end < start)
            throw new ArgumentException("End date must be greater than or equal to start date.");
        Start = start;
        End = end;
    }
    public bool Includes(DateTime date)
    {
        return date >= Start && date <= End;
    }
}
