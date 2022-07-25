using UnityEngine;
using System;
using System.IO;

public class Logger : MonoBehaviour
{
    private const string FileName = "./Logs/LogException.log";

    private StreamWriter getWriter()
    {
        StreamWriter writer;
        if (!File.Exists(FileName))
            writer = File.CreateText(FileName);
        else
            writer = new StreamWriter(File.Open(FileName, FileMode.Append, FileAccess.Write));

        return writer;
    }

    public void Log(string data)
    {
        StreamWriter writer = getWriter();

        writer.WriteLine(data);
        writer.Close();
    }

    public void LogWithTime(string data)
    {
        StreamWriter writer = getWriter();

        writer.WriteLine(GetCurrentDate() + GetCurrentTime() + data);
        writer.Close();
    }

    private string GetCurrentDate()
    {
        string temp = "";
        string date = DateTime.Today.ToString();
        string[] temp2 = new string[3];
        temp2 = date.Split(new Char[] { '/', ' ' });
        temp = temp2[2] + "_" + temp2[0] + "_" + temp2[1] + "-";
        return temp;
    }

    private string GetCurrentTime()
    {
        string temp = "";
        string date = DateTime.Now.ToString();
        string[] temp2 = new string[3];
        temp2 = date.Split(new Char[] { ' ' });
        temp = temp2[1] + " ";
        return temp;
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    void LogCallback(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
            LogWithTime(condition + ": " + stackTrace);
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }
}