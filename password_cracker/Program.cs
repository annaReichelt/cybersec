// See https://aka.ms/new-console-template for more information
using Cybersec;

BruteForcer bf = new BruteForcer();
String pw = "hello";
TimeSpan ts = bf.bruteForcePassword(pw, "abcdefghijklmnopqrstuvwxyz");



Dictionary dict = new Dictionary();
Dictionary<String, int> statdict = dict.createDictionary("C:/Users/Snaggle/ProgramminProjects/Cybersec25/cybersec/password_cracker/passwords1000.txt");

foreach (KeyValuePair<string, int> kvp in statdict) {
	Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
}

dict.crackPasswordUsingDictionary(statdict); 
