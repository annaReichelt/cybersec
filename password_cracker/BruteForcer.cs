using System;

namespace Cybersec {
    public class BruteForcer {
        public BruteForcer() { }

        public bool bruteForcePassword(String password, String alphabet) {
            // Brute force the password provided using the given alphabet
            // Return true if the password is cracked, false otherwise

            int length = password.Length;
            char[] alphabetArray = alphabet.ToCharArray();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

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
                    Console.WriteLine("Time taken: " + stopwatch.Elapsed);
                    return true;
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Password not cracked.");
            Console.WriteLine("Time taken: " + stopwatch.Elapsed);
            return false;
        }

        /// <summary>
        /// Takes a given regex/pattern and returns every character this pattern can fit in a char array
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public char[] getAlphabet(String pattern) {

            char[] alphabet = new char[0];
            for (int i = 0; i < pattern.Length; i++) {
                if (pattern[i] == '[') {
                    i++;
                    while (pattern[i] != ']') {
                        char[] temp = new char[alphabet.Length + 1];
                        for (int j = 0; j < alphabet.Length; j++) {
                            temp[j] = alphabet[j];
                        }
                        temp[alphabet.Length] = pattern[i];
                        alphabet = temp;
                        i++;
                    }
                }
                else {
                    char[] temp = new char[alphabet.Length + 1];
                    for (int j = 0; j < alphabet.Length; j++) {
                        temp[j] = alphabet[j];
                    }
                    temp[alphabet.Length] = pattern[i];
                    alphabet = temp;
                }
            }

            Console.WriteLine(alphabet);
            return alphabet;

        }

    }
}