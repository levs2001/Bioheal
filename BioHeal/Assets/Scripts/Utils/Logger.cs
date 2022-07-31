using System;
using System.IO;

public static class Logger
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
        string temp = "";
        string date = DateTime.Today.ToString();
        string[] temp2 = new string[3];
        temp2 = date.Split(new Char[] { '/', ' ' });
        temp = temp2[2] + "_" + temp2[0] + "_" + temp2[1] + "-";
        return temp;
    }

    private static string GetCurrentTime()
    {
        string temp = "";
        string date = DateTime.Now.ToString();
        string[] temp2 = new string[3];
        temp2 = date.Split(new Char[] { ' ' });
        temp = temp2[1] + " ";
        return temp;
    }
}