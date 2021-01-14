using System.Threading.Tasks;

namespace Desorganizze.Infra.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<Result> ExecuteAsync(TCommand command);
    }
}
