# SharpBruteForceSSH
This is a simple SSH brute force tool written in C#. It is designed to perform dictionary-based brute force attacks on SSH services. The tool takes a target IP address, a list of usernames, and a list of passwords as input. It then attempts to authenticate using each combination of username and password until a successful login is found or all combinations have been exhausted.

## Features:

Supports multithreaded brute force attacks for faster execution.
Allows setting a delay between authentication attempts to avoid lockouts or rate limiting.
Provides an option to continue the brute force attack even after finding a valid username and password combination.
Retrieves the SSH server banner for target identification.
Written using the Renci.SshNet library for SSH communication.

## Compiled:

```sh
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  /reference:Renci.SshNet.dll /out:SharpBruteForceSSH.exe .\SharpBruteForceSSH.cs
```
![image](https://github.com/HernanRodriguez1/SharpBruteForceSSH/assets/66162160/90e4fc5b-cb89-43fb-ab35-912cd9037b3b)


## Usage
```sh
.\SharpBruteForceSSH.exe 192.168.200.130 .\username.txt .\password.txt -delay 2 -threads 5  
```
![image](https://github.com/HernanRodriguez1/SharpBruteForceSSH/assets/66162160/a0e09536-4db4-4a7a-952b-6dbf4be9022c)

```sh
.\SharpBruteForceSSH.exe 192.168.200.130 .\username.txt .\password.txt -delay 2 -threads 5  --continue
```
![image](https://github.com/HernanRodriguez1/SharpBruteForceSSH/assets/66162160/268719c4-4914-4197-8ff3-118688a9835b)

Added the option to validate the status of the service if it is available or not. Example of SSH service turned off or IP blocking in the middle of a brute force process
![image](https://github.com/HernanRodriguez1/SharpBruteForceSSH/assets/66162160/c8b76ebd-847b-49f9-8bb2-cfb112b85532)
