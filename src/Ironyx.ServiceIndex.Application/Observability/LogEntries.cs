using Microsoft.Extensions.Logging;

namespace Ironyx.ServiceIndex.Application.Observability
{
    public partial class RegisterCommandHandlerLogEntries(ILogger<RegisterCommandHandler> logger)
    {
        [LoggerMessage(LogLevel.Debug, message: "Registering {Service} service")]
        public partial void LogRegistering(string service);

        [LoggerMessage(LogLevel.Information, message: "{Service} service has been registered")]
        public partial void LogRegistered(string service);
    }

    public partial class GetRegistrationsQueryHandlerLogEntries(ILogger<GetRegistrationsQueryHandler> logger)
    {
        [LoggerMessage(LogLevel.Debug, message: "Querying service registrations")]
        public partial void LogQueryingRegistrations();
        [LoggerMessage(LogLevel.Information, message: "{Count} service registration(s) has been queried")]
        public partial void LogQueryedRegistrations(int count);
    }
}
