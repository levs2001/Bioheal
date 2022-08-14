using System;

public interface Log
{
    public void Error(Exception exception);

    public void Error(string message);

    public void Error(Exception exception, string message);

    public void Warn(string message);
}