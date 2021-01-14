namespace Desorganizze.Infra.CQRS.Commands
{
    public class Result
    {
        private static readonly Result OkResult = new Result(true, string.Empty);

        public string ErrorMessage { get; }
        public bool Success { get; }
        public bool Failure { get { return !Success; } }

        public Result(bool isSuccess, string errorMessage)
        {
            Success = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Ok()
        {
            return OkResult;
        }

        public static Result Fail(string errorMessage)
        {
            return new Result(false, errorMessage);
        }
    }
}
