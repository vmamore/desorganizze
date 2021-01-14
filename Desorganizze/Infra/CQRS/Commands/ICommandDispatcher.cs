using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task<Result> ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
