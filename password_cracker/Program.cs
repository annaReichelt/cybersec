// See https://aka.ms/new-console-template for more information
using Cybersec;

BruteForcer bf = new BruteForcer();
//bf.bruteForcePassword("123419", "0123456789");

Dictionary dict = new Dictionary();
Dictionary<String, int> statdict = dict.createDictionary("C:/Users/annam/Documents/Uni/Cybersec/password_cracker/passwords1000.txt");

foreach (KeyValuePair<string, int> kvp in statdict) { //TODO: Numbers are off, fix this
    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
}
