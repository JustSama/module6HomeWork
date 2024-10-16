using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class ConfigurationManager
{
    private static ConfigurationManager instance;
    private static readonly object lockObj = new object();
    private Dictionary<string, string> settings = new Dictionary<string, string>();

    private ConfigurationManager() { }

    public static ConfigurationManager GetInstance()
    {
        if (instance == null)
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = new ConfigurationManager();
                }
            }
        }
        return instance;
    }

    public void LoadSettings(string filePath)
    {
        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=');
            if (parts.Length == 2)
            {
                settings[parts[0].Trim()] = parts[1].Trim();
            }
        }
    }

    public void SaveSettings(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in settings)
            {
                writer.WriteLine($"{entry.Key}={entry.Value}");
            }
        }
    }

    public string GetSetting(string key)
    {
        return settings.ContainsKey(key) ? settings[key] : null;
    }

    public void SetSetting(string key, string value)
    {
        settings[key] = value;
    }
}

class Program
{
    static void Main()
    {
        var config1 = ConfigurationManager.GetInstance();
        var config2 = ConfigurationManager.GetInstance();
        Console.WriteLine(ReferenceEquals(config1, config2));

        config1.SetSetting("Theme", "Dark");
        config1.SetSetting("Language", "English");
        config1.SaveSettings("config.txt");

        config2.LoadSettings("config.txt");
        Console.WriteLine(config2.GetSetting("Theme"));
        Console.WriteLine(config2.GetSetting("Language"));

        Thread t1 = new Thread(() => TestSingleton("T1"));
        Thread t2 = new Thread(() => TestSingleton("T2"));
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }

    static void TestSingleton(string threadName)
    {
        var config = ConfigurationManager.GetInstance();
        Console.WriteLine($"{threadName}: {config.GetHashCode()}");
    }
}
