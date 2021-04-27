using Serilog;
using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Commands
{
    public class LoggingHandler<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> next;

        public LoggingHandler(ICommandHandler<T> next)
        {
            this.next = next;
        }

        public Task<Result> ExecuteAsync(T command)
        {
            Log.Information("Starting {@handler} with command: {@command}", next.GetType(), command);
            return next.ExecuteAsync(command);
        }
    }
}
