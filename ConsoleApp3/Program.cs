using System;
using System.Collections.Generic;
using System.Linq;

public class Servers
{
    private static readonly Lazy<Servers> _instance = new Lazy<Servers>(() => new Servers());
    private readonly HashSet<string> _servers;

    private Servers()
    {
        _servers = new HashSet<string>();
    }

    public static Servers Instance => _instance.Value;

    public bool AddServer(string serverAddress)
    {
        if (string.IsNullOrEmpty(serverAddress))
            return false;

        if (!serverAddress.StartsWith("http://") && !serverAddress.StartsWith("https://"))
            return false;

        return _servers.Add(serverAddress);
    }

    public IEnumerable<string> GetHttpServers()
    {
        return _servers.Where(s => s.StartsWith("http://")).ToList();
    }

    public IEnumerable<string> GetHttpsServers()
    {
        return _servers.Where(s => s.StartsWith("https://")).ToList();
    }
}

// Пример использования
public class Program
{
    public static void Main()
    {
        var servers = Servers.Instance;

        Console.WriteLine(servers.AddServer("http://example.com")); // True
        Console.WriteLine(servers.AddServer("https://secure.com")); // True
        Console.WriteLine(servers.AddServer("ftp://files.com"));    // False
        Console.WriteLine(servers.AddServer("http://example.com")); // False (duplicate)

        Console.WriteLine("HTTP Servers:");
        foreach (var server in servers.GetHttpServers())
        {
            Console.WriteLine(server);
        }

        Console.WriteLine("HTTPS Servers:");
        foreach (var server in servers.GetHttpsServers())
        {
            Console.WriteLine(server);
        }
    }
}
 