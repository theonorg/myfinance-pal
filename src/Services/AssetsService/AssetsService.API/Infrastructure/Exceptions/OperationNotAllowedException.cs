namespace Tiberna.MyFinancePal.AssetsService.API.Infrastructure.Exceptions;

public class OperationNotAllowedException : Exception
{
    private OperationNotAllowedException() : base()
    {

    }
    public OperationNotAllowedException(string? message) : base(message)
    {
    }

    public OperationNotAllowedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected OperationNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}