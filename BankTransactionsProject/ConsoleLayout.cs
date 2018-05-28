using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace BankTransactionsProject
{
    class ConsoleLayout
    {

        public static void Initialmethod()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Login.EncryptPasswordOnSQL();
        }
            

        public static void Position(int i)
        {
            int leftOffSet = (Console.WindowWidth / 3);
            int topOffSet = (Console.WindowHeight / 3) + i;
            Console.SetCursorPosition(leftOffSet, topOffSet);
        }


        public static void Attention(string warning, int i)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.Red;
            Position(i);
            Console.WriteLine(warning);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Speech(string message)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult); // to change VoiceGender and VoiceAge check out those links below
                synth.Volume = 100;  // (0 - 100)
                synth.Rate = 0;     // (-10 - 10)
                //synth.SetOutputToDefaultAudioDevice();  // Configure the audio output.                                                        
                synth.SpeakAsync(message); // Speak a string.                  
                Console.WriteLine();
                Console.ReadKey();                
            }
        }

        public static void Speech(string message, int rate)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult); // to change VoiceGender and VoiceAge check out those links below
                synth.Volume = 100;  // (0 - 100)
                synth.Rate = rate;     // (-10 - 10)
                //synth.SetOutputToDefaultAudioDevice();  // Configure the audio output.                                                        
                synth.Speak(message); // Speak a string.  
                //Console.WriteLine();
                //Console.ReadKey();
            }
        }
    }
}
