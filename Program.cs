using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DgDerleme
{
    internal class Program
    {
        public enum CommandTurleri
        {
            Dogum_Gunleri_Hepsi,
            Dogum_Gunleri_Alfabetik,
            Yaklasan_Gunler,
            Bu_Ve_Gelecek_Ay_Gunleri,
            Kisi_Ara,
            Kisi_Ekle,
            Ayarlar,
            Cikis,
        }
        static int selectedItemIndexCommands = 0;


        [STAThread]
        static void Main(string[] args)
        {

            GetCommand();

            Console.WriteLine("Burdan Sonra Bisey Kodlanmamis.");
            Console.WriteLine("Cikmak Icin bi Tusa bas ltfn");
            Console.ReadKey();
        }





        public static void GetCommand()
        {
            Console.Clear();

            CommandTurleri[] values = (CommandTurleri[])Enum.GetValues(typeof(CommandTurleri));

            Console.CursorVisible = false;



            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Aksiyon secin:");
                Console.ResetColor();


                for (int i = 0; i < values.Length; i++)
                {
                    if (i == selectedItemIndexCommands)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("=> " + values[i]);

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(values[i]);
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow && selectedItemIndexCommands > 0)
                {
                    selectedItemIndexCommands--;
                    //Console.Beep();
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && selectedItemIndexCommands < values.Length - 1)
                {
                    selectedItemIndexCommands++;
                    //Console.Beep();
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    try
                    {
                        //string functionName = values[selectedItemIndexCommands].ToString();


                        CallCommand(values[selectedItemIndexCommands]);

                        break;

                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Tanimlanan Index hatasi. Muhtemelen Function Adini Yanlis Yazmisimdir GetCommand()");
                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow && selectedItemIndexCommands == 0)
                {
                    selectedItemIndexCommands = values.Length - 1;
                    //Console.Beep();
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && selectedItemIndexCommands == values.Length - 1)
                {
                    selectedItemIndexCommands = 0;
                    //Console.Beep();
                }
            }

            Console.CursorVisible = true;
        }


        static void CallCommand(CommandTurleri _Input)
        {
            //Console.WriteLine(_Input.ToString());


            switch (_Input)
            {
                case CommandTurleri.Dogum_Gunleri_Hepsi:
                    new Dogum_Gunleri_Hepsi_Sayfa();
                    break;
                case CommandTurleri.Kisi_Ekle:
                    new Kisi_Ekle_Sayfa();
                    break;
            }
            return;
        }


        static void GetPrevs()
        {
            //Burayi Gec apple da farklidir muhtemelen

            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dates");
            List<DogumGunuInfoClass> myClassList = new List<DogumGunuInfoClass>();

            // Check if the "dates" folder exists
            if (Directory.Exists(folderPath))
            {
                string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

                foreach (string jsonFile in jsonFiles)
                {
                    try
                    {
                        string jsonContent = File.ReadAllText(jsonFile);
                        DogumGunuInfoClass myObject = JsonConvert.DeserializeObject<DogumGunuInfoClass>(jsonContent);
                        myClassList.Add(myObject);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading JSON file {jsonFile}: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Folder 'dates' does not exist.");
            }

            // Print the list of objects
            foreach (DogumGunuInfoClass myObject in myClassList)
            {
                Console.WriteLine($"Date: {myObject.Date}");
                Console.WriteLine($"Name: {myObject.Name}");
                Console.WriteLine($"Notes: {myObject.Notes}");
                Console.WriteLine($"Two Week Warning: {myObject.Two_Week_Warning}");
                Console.WriteLine($"Entry Date: {myObject.Entry_Date}");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
