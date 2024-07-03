using CQRS.Application.Common.Interfaces.Services;

namespace CQRS.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

