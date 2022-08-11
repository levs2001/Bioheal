using System;

public static class LogFactory
{
    public static Log GetLog(Type classType)
    {
        return new LogImpl(classType);
    }
}