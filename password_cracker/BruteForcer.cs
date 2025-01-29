using System;
using System.Diagnostics;

namespace Cybersec {
	public class BruteForcer {
		public BruteForcer() { }
		public TimeSpan bruteForcePassword(String password, String alphabet) {

			int length = password.Length;
			char[] alphabetArray = alphabet.ToCharArray();
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			Console.WriteLine("Trying to brute force password: " + password);

			for (int i = 0; i < Math.Pow(alphabet.Length, length); i++) {
				String guess = "";
				int j = i;
				while (j > 0) {
					guess = alphabet[j % alphabet.Length] + guess;
					j = j / alphabet.Length;
				}
				while (guess.Length < length) {
					guess = alphabet[0] + guess;
				}
				if (guess.Equals(password)) {
					stopwatch.Stop();
					Console.WriteLine("Password cracked: " + guess);
					Console.WriteLine("Time taken Brute Force: " + stopwatch.Elapsed);
					return stopwatch.Elapsed;
				}
				if (stopwatch.Elapsed.TotalMinutes > 5) {
					stopwatch.Stop();
					return new TimeSpan(0);
				}
			}

			stopwatch.Stop();
			return new TimeSpan(0);
		}



		public bool bruteForcePasswordUsingHint(String password, char[] hint, String alphabet) {
			int hintLength = hint.Length;
			int passwordLength = password.Length;
			char[] alphabetArray = alphabet.ToCharArray();
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			Console.WriteLine("Trying to crack password: " + password + " with hint: " + new string(hint));

			for (int i = 0; i <= passwordLength - hintLength; i++) {
				for (int g = 0; g < Math.Pow(alphabet.Length, passwordLength - hintLength); g++) {
					String guess = "";
					int j = g;
					while (j > 0) {
						guess = alphabet[j % alphabet.Length] + guess;
						j = j / alphabet.Length;
					}
					while (guess.Length < passwordLength - hintLength) {
						guess = alphabet[0] + guess;
					}
					guess = guess.Insert(i, new string(hint));
					if (guess.Equals(password)) {
						stopwatch.Stop();

						TimeSpan bfTime = bruteForcePassword(password, "0123456789");
						if (bfTime == new TimeSpan(0)) {
							Console.WriteLine("Password not cracked with reduced alphabet");
							bfTime = bruteForcePassword(password, "abcdefghijklmnopqrstuvwxyz");
						}
						if (bfTime == new TimeSpan(0)) {
							Console.WriteLine("Password not cracked with reduced alphabet");
							bfTime = bruteForcePassword(password, "0123456789abcdefghijklmnopqrstuvwxyz");
						}

						using (StreamWriter sw = File.AppendText("C:/Users/Snaggle/ProgramminProjects/Cybersec25/cybersec/password_cracker/experiments.csv")) {
							sw.WriteLine($"{password};{stopwatch.Elapsed};{new string(hint)};{bfTime}");
							sw.Close();
						}

						return true;
					}
					else if (stopwatch.Elapsed.TotalMinutes > 5) {
						stopwatch.Stop();
						return false;
					}
				}
			}

			stopwatch.Stop();
			return false;
		}
	}
}