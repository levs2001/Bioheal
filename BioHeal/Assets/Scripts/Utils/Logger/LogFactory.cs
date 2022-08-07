using System;

public static class LogFactory
{
    static public Log GetLog(Type classType)
    {
        return new LogImpl(classType);
    }
}