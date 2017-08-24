using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Vuture.CodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // input text to be used across all tasks
            String inputText = "My dog likes chicken, but hates the smell of cheese.";

            // output instructions, on how to run each task
            Console.WriteLine("Please select what task you wish to run.");
            Console.WriteLine("Enter 1 for task 1, 2 for task 2 or 3 for task 3:");

            // prompt the user to select an option
            int option;
            String input = Console.ReadLine();

            // if non-number is entered, re-prompt for a numerical value
            while (Regex.IsMatch(input, "[0-9]") == false)
            {
                Console.WriteLine("Input Error. Please enter a correct number:");
                input = Console.ReadLine();
            }

            // convert entered string to integer and store in 'option' variable
            option = Convert.ToInt32(input);

            // switch case
            switch (option)
            {
                // if '1' entered, load task 1
                case 1:
                    Console.WriteLine("Task 1 selected");
                    Task1 myTask1 = new Task1();
                    Console.WriteLine("Input text: "+ inputText);
                    Console.WriteLine("Character currently selected: " + "e");
                    Console.WriteLine("Number of instances: " + myTask1.calculateInstances("e", inputText));
                    break;

                // if '2' entered, load task 2
                case 2:
                    Console.WriteLine("Task 2 selected");
                    Task2 myTask2 = new Task2();
                    Console.WriteLine("Input text: " + inputText);
                    myTask2.getUserInput(inputText);
                    break;

                // if '3' entered, load task 3
                case 3:
                    Console.WriteLine("Task 3 Comprises of Three parts. Each one will run in succession i.e. Part A will run first.");

                    // loading part A
                    Console.WriteLine("Part A:");
                    Task3A partA = new Task3A();
                    partA.getWordInstances(inputText);

                    Console.WriteLine();
                    
                    // loading part B
                    Console.WriteLine("Part B:");
                    Task3B partB = new Task3B();
                    partB.censorWords(inputText);

                    Console.WriteLine();

                    // loading part C
                    Console.WriteLine("Part C:");
                    Task3C partC = new Task3C();
                    String palindromeText = "Anna went to vote in the election to fulfil her civic duty";
                    Console.WriteLine("Input: " + palindromeText);
                    Console.WriteLine("Palindrome Censored Output: " + partC.censorPalindrome(palindromeText));
                    break;

                default:
                    // if incorrect option selected, prompt the user to close and re-run application
                    Console.WriteLine("Incorrect input entered. Please exit and re-run application.");
                    break;
            }

        }
    
    }

    /* Task 1 begins here
     */
    class Task1
    {
        // function calculates # of occurences of a character/s in the 'input' string and returns this as a number (integer)
        public int calculateInstances(String chars, String input)
        {
            // regex pattern
            String strRegexPattern = "(?i)" + chars;

            // using 'Regex' class 'Matches' function to return occurences of 'chars' string in 'input' string  
            return Regex.Matches(input, strRegexPattern).Count;
        }
    }

    /* Task 2 begins here
     */
    class Task2
    {
        // function transforms 'word' string and outputs if it's a palindrome
        public void getUserInput(String word)
        {
            // calls 'stripString' function to remove special characters, whitespace and change all capitals to lower case - for easier comparison
            String strippedString = stripString(true, word);

            // calls 'reversedString' to reverse 'strippedString' variable
            String reversedString = reverseString(strippedString);

            // output result to the console
            Console.WriteLine("Input Text Palindrome: "+ isPalindrome(strippedString, reversedString));
        }

        // removing special characters, removing white space and transforming caps to lower case and returns the result
        public String stripString(bool formatString, String input)
        {
            // regex pattern
            String specialCharsRegexPattern = "[!'.@)(£$%^&*-=+_,/@~#]";

            // variable to hold the result of removing special characters from 'input' string using the 'Regex' library, 'Replace' function
            String replaced = Regex.Replace(input, specialCharsRegexPattern, "");

            // checks if string formatting has been requested
            if(formatString == true)
            {
                // removes all whitepspace from 'replaced' string
                replaced = replaced.Replace(" ", "");

                // transforms all capitals to lower case
                replaced = replaced.ToLower();
            }

            // returns 'replaced' string
            return replaced;
        }

        // function to reverse a string and return the result
        public String reverseString(String str)
        {
            // local variable to hold reversed 'str' string
            String result="";

            // loops through 'str', starting from the end of the string to the start
            for (int i=str.Length; i!=0; i--)
            {
                // appends to 'result' string
                result += str[i-1];
            }

            // returns reveresed 'result' string
            return result;
        }

        // function which compares original input and reversed input and returns true if palindrome, otherwise false if not
        public bool isPalindrome(String original, String reversed)
        {
            // local variable to help with palindrome calculation
            int counter = 0;

            // loops through each 'original' string entry and compares with 'reversed' string
            for(int i=0; i < original.Length; i++)
            {
                // checks if both string entries are identical
                if(original[i] == reversed[i])
                {
                    // if identical then increment 'counter' variable
                    counter++;
                }
            }

            // checks if 'counter' variable equals the 'original' variable. If it does, then returns true, otherwise returns false 
            if(counter == original.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    /* Task 3 part A begins here
     */
    class Task3A
    {
        // censor words stored in an array.
        public String[] censoredWordList = new String[] {"cheese", "dog", "chicken"};

        // object declaration to re-use existing code from task1 class
        public Task1 instanceHelper = new Task1();

        // using existing Task1 class & function to calculate instances of censored word/s
        public void getWordInstances(String text)
        {
            // 'counter' variable to hold occurences of words in input 'text' string  
            int counter = 0;

            // output input to the console
            Console.WriteLine("Input: " + text);
            Console.WriteLine("Word List: " + getCensoredWordList());
            // goes through each censoredWordList array entry and checks for the occurrence of words
            for (int i=0; i<censoredWordList.Length; i++)
            {
                // calls function in task1 class, to calculate # of occurences of current array entry in the 'text' string
                counter = instanceHelper.calculateInstances(censoredWordList[i], text);

                // outputs to the console the # of occurences of each word
                Console.WriteLine("Word: "+ censoredWordList[i] + ", Number of Occurrences: "+ counter);

                // resets counter
                counter = 0;
            }

        }

        // returns all words from 'censoredWordList' array as a combined string
        public String getCensoredWordList()
        {
            String wordListString = "{";
            for(int i=0; i<censoredWordList.Length; i++)
            {
                wordListString += censoredWordList[i] + ", ";
            }
            wordListString += "}";

            return wordListString;
        }
    }

    /* Task 3 part B begins here
     */
    class Task3B
    {
        // objects from other tasks to make task easier
        public Task3A myHelper = new Task3A();
        public Task2 stripStringHelper = new Task2();

        // function detects and sets about censoring input string
        public void censorWords(String input)
        {
            String stringHelper = "";
            String[] splitText = new String[] {};
            String[] splitParam = new String[] {" "};
            String result = "";
            int counter = 0;

            // splits input string where there are whitespaces and stores it in an array
            splitText = input.Split(splitParam, StringSplitOptions.None);

            // loops through array and checks the presence of censored words.
            for(int i=0; i<splitText.Length; i++)
            {
                // removes special characters from array entry string and stores in stringHelper string
                stringHelper = stripStringHelper.stripString(false, splitText[i]);

                // loops through censoredwordList and checks against array entry
                for(int j=0; j<myHelper.censoredWordList.Length; j++)
                {
                    // checks for occurence of censor word in array entry against censoredWordList.
                    if(myHelper.instanceHelper.calculateInstances(myHelper.censoredWordList[j], stringHelper) >0)
                    {
                        // if censor word occurence found, getCensoredWord function called to return censored value and appends to final result
                        result += getCensoredWord(splitText[i]) + " ";

                        // increase counter to indicate found presense of a censor word.
                        counter++;
                    }
                }

                // checks if there were any occurences in the current array entry
                if(counter == 0)
                {
                    // if there wore none then append a whitespace
                    result += splitText[i] + " ";
                }
                // reset counter
                counter = 0;
            }
            // output to console
            Console.WriteLine("Input: " + input);
            Console.WriteLine("Censor Words List: " + myHelper.getCensoredWordList());
            Console.WriteLine("Censored Output: " + result);
        }

        // returns a censored word
        public String getCensoredWord(String text)
        {
            // helper string variables
            String newWord = "";
            String censorChars = "";
            String holder = "";
            String result = "";
            String regex = "[!£$%^&*)(-=_+'@#;.,?/]";

            // removes special characters from text variable
            newWord = Regex.Replace(text, regex, "");

            // gets the in-between text of words and holds them in holder string.
            for (int i=1; i<newWord.Length-1;i++)
            {
                holder += newWord[i].ToString();
            }

            // calculates number of special characters
            for (int j=0; j< holder.Length; j++)
            {
                censorChars += "$";
            }

            // replaces text variable with censored string
            result = text.Replace(holder, censorChars);

            return result;
        }

    }

    /* Task 3 part C begins here
     */
    class Task3C
    {
        // object declarations, making use of Task2 and Task3B classes to simply solution
        Task2 task2Helper = new Task2();
        Task3B partBHelper = new Task3B();

        // String array to store result of splitting strings
        String[] splitString = new String[] { };

        // String array to be used as 'Split Meter' for the 'String.Split' library & function
        String[] splitParam = new String[] { " " };

        // variable to hold final result
        String result = "";

        // helper variables to aid in calculating & returning result
        String helperString = "";
        int counter = 0;

        // function which detects palindrome words and censors them - returns result of censoring
        public String censorPalindrome(String input)
        {
            // splits the 'input' string and stores in 'splitString' array
            splitString = input.Split(splitParam, StringSplitOptions.None);

            // holds the 'input' variable
            result = input;

            // loops through each 'splitString' array entry
            for(int i=0; i<splitString.Length; i++)
            {
                // make use of 'stripString' function in 'Task2' class to remove whitespace, special characters and change capitals to lower case,
                // the result is stored in 'helperString' string
                helperString = task2Helper.stripString(true, splitString[i]);

                // checks if current array entry is a palindrome
                if(task2Helper.isPalindrome(helperString, task2Helper.reverseString(helperString)) == true)
                {
                    // if it is, then the word is censored using the 'getCensoredWord' function in the 'Task3B' class,
                    // then the original entry of the palindrome word is replaced by the result of this function.
                    // the 'result' variable is updated each time this happens.
                    result = result.Replace(splitString[i], partBHelper.getCensoredWord(splitString[i]));

                    // 'counter' is incremented to indicate the presence of a palindrome word
                    counter++;
                }
            }

            // checks if there were any palindrome words present in the 'input' string
            if (counter == 0)
            {
                // if there were none, then a default message is stored in the 'result' string
                result = "No Palindrome words detected.";
            }
            
            // return 'result' string
            return result;
        }

    }
}

