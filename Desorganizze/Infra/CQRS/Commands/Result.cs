namespace Desorganizze.Infra.CQRS.Commands
{
    public class Result
    {
        public string ErrorMessage { get; }
        public bool Success { get; }
        public bool Failure { get { return !Success; } }
        public object ReturnDto { get; }

        public Result(bool isSuccess, string errorMessage, object returnDto = null)
        {
            Success = isSuccess;
            ErrorMessage = errorMessage;
            ReturnDto = returnDto;
        }

        public static Result Ok(object returnDto = null)
        {
            return new Result(true, string.Empty, returnDto);
        }

        public static Result Fail(string errorMessage)
        {
            return new Result(false, errorMessage);
        }
    }
}
