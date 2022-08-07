using System;

public class LogImpl : Log
{
    private static const string ERROR = "Error: ";
    private static const string WARNING = "Warning: ";
    private static const string MESSAGE = "\n\tMessage: ";
    private static const string EXCEPTION_MESSAGE = "\n\tException: ";
    private static const string CALLER = "\n\tCaller: ";

    public Type classType;

    public LogImpl(Type classType)
    {
        this.classType = classType;
    }

    public void Error(Exception exception)
    {
        Error(exception, "");
    }

    public void Error(string message)
    {
        Error(null, message);
    }

    public void Error(Exception exception, string message)
    {
        string exceptionMessage = "";

        if (exception != null)
            exceptionMessage = exception.ToString();

        LogWriter.LogWithTime(ERROR + 
                                MESSAGE + message + 
                                EXCEPTION_MESSAGE + exceptionMessage + 
                                CALLER + classType.Name);
    }

    public void Warn(string message)
    {
        LogWriter.LogWithTime(WARNING + 
                                MESSAGE+ message + 
                                CALLER + classType.Name);
    }
}