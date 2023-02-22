namespace Text_Projects_Menu {
    internal class Program {
        static void Main(string[] args) {
            int userSelection = -1;

            while (userSelection != 0) {
                userSelection = Menu();

                switch (userSelection) {
                    case 1: { WordSeparator(); break; }
                    case 2: { PigLatin(); break; }
                    case 3: { MorseCodeConverter(); break; }
                    case 4: { FileEncyption(); break; }
                    case 5: { FileDecryption(); break; }

                    default: { Console.WriteLine("Thank you for using my program"); break; }
                }//end menu selection switch statement
                if (userSelection != 0) {
                    Input("Press enter to go back to the menu");
                }//end if statement
            }//end while
        }//end main

        static string Input(string prompt) {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
        static int Menu() {
            bool parseSuccessful = false;
            string userInput;
            int userSelection;

            do {
                Console.Clear();
                Console.WriteLine("Please make a selection from the menu");
                Console.WriteLine("1. Word Separator");
                Console.WriteLine("2. Pig Latin");
                Console.WriteLine("3. Morse Code Converter");
                Console.WriteLine("4. File Encryption");
                Console.WriteLine("5. File Decryption");

                userInput = Input("Program Selection");
                parseSuccessful = int.TryParse(userInput, out userSelection);
            } while (parseSuccessful = false || userSelection < 0 || userSelection > 6);

            return userSelection;
        }//end menu


        static void WordSeparator() {//Try to implement string split into this over the weekend



            //Create file to save info to
            FileStream outfile = new FileStream("C:\\Users\\BSiler\\Documents\\coolfile.txt", FileMode.OpenOrCreate);
            Console.Clear();//For better viewing once put into a menu
            //Variables and taking in input
            string userInput = Input("Please enter a statement here");
            string newString = "";



            //Store input taken into a char array so that you may go through the index inside a for loop
            char[] charArray = userInput.ToCharArray();
            //Store the first capital letter of the arrays index into the newstring so it wont be apart of the letters the goes lowercase or gets a space in front of it
            newString += charArray[0];



            for (int index = 1; index < charArray.Length; index++) {
                if (charArray[index] >= 'A' && charArray[index] <= 'Z') {
                    //If the index reaches a capital word then take that new word and store it into newstring array with a space and its lowercase counterpart
                    newString += " " + (char)(charArray[index] - 'A' + 'a'); //Space before the uppercase char and then cast it to a char and minus the 32 to get the upperdcase letter to its lowercase counter part         
                } else {
                    newString += charArray[index];
                }// End if statement
            }//End for loop that runs through the index of the char Array



            char[] buffer = newString.ToCharArray();



            for (int i = 1; i < buffer.Length; i++) {
                byte data = (byte)buffer[i];//Convert data value to byte type using a cast
                outfile.WriteByte(data);// Wrtie byte to file                                                
            }//End data nested for loop
            Console.WriteLine(buffer);



            //Close file
            outfile.Close();
        }//End Word separator Function


        static void PigLatin() {
            string completedText = "";
            const string VOWELS = "AEIOU";
            string newStatement = "";
            FileStream infile = new FileStream("C:\\Users\\BSiler\\Documents\\Pig Latin.txt", FileMode.Open);



            Console.Clear();



            Console.WriteLine("Pig Latin");



            Console.WriteLine("\nThe text in the current file is: \n");
            //Going through the file one byte at a a time
            while (infile.Position < infile.Length) {



                //read and store a byte from infile;
                byte currentByte = (byte)infile.ReadByte();

                //WRITE CURRENT BYTE AS CHAR
                Console.Write((char)currentByte);



                //STORE CURRENT BYTE AS CHAR IN STRING
                completedText += (char)currentByte;
            }//END WHILE



            Input("\nPress enter to see this translated to Pig Latin\n");



            //SPLIT STRING INTO WORDS USING SPACE
            foreach (string word in completedText.Split(' ')) {

                string firstLetter = word.Substring(0, 1);
                string restOfWord = word.Substring(1, word.Length - 1);

                //CHECKS IF FIRST LETTER IS VOWEL OR NOT
                int currentLetter = VOWELS.IndexOf(firstLetter);

                //IF FIRST LETTER IS NOT A VOWEL
                if (currentLetter == -1) {

                    //CHANGE PLACEMENT, THEN ADD "AY"
                    newStatement += (restOfWord + firstLetter + "AY" + " ");

                    // IF FIRST LETTER IS A VOWEL
                } else {

                    //ADD "WAY"
                    newStatement += (word + "WAY" + " ");
                }
            }

            Console.WriteLine(newStatement);



            infile.Close();
        }//end pig latin function
  



        static void MorseCodeConverter() {
            string userInput = "";
            FileStream outfile = new FileStream("C:\\Users\\BSiler\\Documents\\MorseCode.txt", FileMode.OpenOrCreate);
            char[] text = { ' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string[] morseCode = { " ", ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--",
                "-.", "---", ".---.", "--.-", ".-.","...", "-", "..-", "...-", ".--", "-..-", "-.--", "--..", ".----",
                "..---", "...--", "....-", ".....", "-....", " --...", "---..", "----.", "-----" };
            string translation = "";

            Console.Clear();
            Console.WriteLine("Morse Code Converter");
            userInput = Input("Enter a statement to be translated to morse code: ");



            userInput = userInput.ToUpper();



            for (int firstIndex = 0; firstIndex < userInput.Length; firstIndex += 1) {
                for (int secondIndex = 0; secondIndex < 37; secondIndex += 1) {
                    if (userInput[firstIndex] == text[secondIndex]) {
                        translation += morseCode[secondIndex];
                        break;
                    }
                }
            }



            char[] output = translation.ToCharArray();



            for (int index = 0; index < output.Length; index += 1) {
                byte data = (byte)output[index];
                outfile.WriteByte(data);
            }

            outfile.Close();
        }//end morse code


        static void FileEncyption() {
            FileStream infile = new FileStream("C:\\Users\\BSiler\\Documents\\musicmaybe.txt", FileMode.Open);
            FileStream encryptedFile = new FileStream("C:\\Users\\BSiler\\Documents\\NewEncryptedFile1.txt", FileMode.OpenOrCreate);


            //Going through the file one byte at a a time
            while (infile.Position < infile.Length) {



                //read and store a byte from infile;
                byte currentByte = (byte)infile.ReadByte();



                //encrypt currect byte by shifting byte's value
                currentByte += 1;



                //writing enctypted byte into the encrypted file
                encryptedFile.WriteByte(currentByte);



                Console.Write($"{(char)currentByte}");
            }//End WHile



            Console.WriteLine("");



            encryptedFile.Close();
            infile.Close();

        }//End File Encryption function

        static void FileDecryption() {
            FileStream encryptedFile = new FileStream("C:\\Users\\BSiler\\Documents\\NewEncryptedFile1.txt", FileMode.Open);
            FileStream decryptedFile = new FileStream("C:\\Users\\BSiler\\Documents\\FileDeed.txt", FileMode.OpenOrCreate);
            //Console.Clear();



            while (encryptedFile.Position < encryptedFile.Length) {



                byte currentByte = (byte)encryptedFile.ReadByte();



                currentByte -= 1;



                decryptedFile.WriteByte(currentByte);
                Console.Write($"{(char)currentByte}");
            }// End While loop
            Console.WriteLine("");


            
            decryptedFile.Close();



        }//End file decryption function


    }//end class
}//end namespace