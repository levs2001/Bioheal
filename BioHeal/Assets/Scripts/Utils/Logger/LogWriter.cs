using System;
using System.IO;

public static class LogWriter
{
    private const string FILE_NAME = "./Logs/LogException.log";

    private static StreamWriter getWriter()
    {
        StreamWriter writer;
        if (!File.Exists(FILE_NAME))
            writer = File.CreateText(FILE_NAME);
        else
            writer = new StreamWriter(File.Open(FILE_NAME, FileMode.Append, FileAccess.Write));

        return writer;
    }

    public static void LogWithTime(string data)
    {
        StreamWriter writer = getWriter();

        writer.WriteLine(GetCurrentDate() + GetCurrentTime() + data);
        writer.Close();
    }

    private static string GetCurrentDate()
    {
        string date = DateTime.Today.ToString();
        string[] temp = date.Split(new Char[] { '/', ' ' });
        return temp[2] + "_" + temp[0] + "_" + temp[1] + "-";
    }

    private static string GetCurrentTime()
    {
        string date = DateTime.Now.ToString();
        return date.Split(new Char[] { ' ' })[1] + " ";
    }
}