# BreadcrumbsSharpBruteForceSSH
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
.\sshBruteForce.exe 192.168.200.130 .\username.txt .\password.txt -delay 2 -threads 5  
```
![image](https://github.com/HernanRodriguez1/SharpBruteForceSSH/assets/66162160/501f5f30-9867-45d9-b15a-54e36cb5c854)


```sh
.\sshBruteForce.exe 192.168.200.130 .\username.txt .\password.txt -delay 2 -threads 5  --continue
```
![image](https://github.com/HernanRodriguez1/SharpBruteForceSSH/assets/66162160/5124777d-b02a-49a0-8d5a-0540fb184127)
