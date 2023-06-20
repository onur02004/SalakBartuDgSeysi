using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DgDerleme
{
    //Public ve static degil ki heryerden referanssiz ulasabil
    public class InputClass
    {
        /// <summary>
        /// Null Cevirmesi => False Input/No Input (Bos string)
        /// "%20" Cevirmesi => Islem Iptali (False)
        /// return => input string degeri
        /// </summary>
        /// <param name="COMMENT"></param>
        /// <param name="HEADER"></param>
        /// <param name="SAYFAADI"></param>
        /// <returns></returns>
        public static string GetStringFromUser(string COMMENT, string HEADER = "String_INPUT(COMMENT,HEADER,SAYFAADI)", string SAYFAADI = "Sayfa_Adi")
        {
            int SelectedIndex = 0;
            Console.CursorVisible = false;
            string name = "";
            ConsoleKeyInfo keyInfo;

            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(SAYFAADI);
                Console.ResetColor();
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(HEADER);
                Console.ResetColor();
                Console.WriteLine(COMMENT);
                Console.WriteLine(name);
                Console.WriteLine();
                Console.WriteLine();
                Console.ResetColor();
                if (SelectedIndex == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("=> Onayla (Enter)");
                    Console.ResetColor();
                    Console.WriteLine("Iptal");
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine("Onayla");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine("=> Iptal (Enter)");
                }
                Console.ResetColor();
                Console.WriteLine();

                keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (SelectedIndex == 0)
                    {
                        if (name.Length > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                // Process the name
                                //DOGUMGUNU.Name = name;
                                //ShowInfo();
                                return name;
                                break;
                            }
                            else
                            {
                                //GetName();
                                return null;
                            }

                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        //ShowInfo();
                        return "%20";
                        break;

                    }
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow && SelectedIndex == 1)
                {
                    SelectedIndex = 0;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && SelectedIndex == 0)
                {
                    SelectedIndex = 1;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (name.Length > 1)
                        name = name.Remove(name.Length - 1, 1);
                    else
                        name = "";
                }
                else if (keyInfo.KeyChar != '\u0000')
                {
                    // Capture the input characters to form the name string
                    name += keyInfo.KeyChar;
                }


            } while (keyInfo.Key != ConsoleKey.Enter);

            return null;
        }


        /// <summary>
        /// Aciklama => baslik olarak yukarida yazar
        /// return => bool value
        /// Null != uygun
        /// </summary>
        /// <param name="Aciklama"></param>
        /// <returns></returns>
        public static bool OnayYesNo(string Aciklama)
        {
            //Yes => True
            //No => False
            bool FINAL = false;
            string[] Ops = { "Yes", "No", "Main_Menu" };
            int SelectedIndex = 0;


            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Dikkat Onay Gerekiyor:\n" + Aciklama.ToUpper());
                Console.ResetColor();


                for (int i = 0; i < Ops.Length; i++)
                {
                    if (i == SelectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("=> " + Ops[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(Ops[i]);
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                }


                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow && SelectedIndex > 0)
                {
                    SelectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && SelectedIndex < Ops.Length - 1)
                {
                    SelectedIndex++;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow && SelectedIndex == 0)
                {
                    SelectedIndex = Ops.Length - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && SelectedIndex == Ops.Length - 1)
                {
                    SelectedIndex = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Program.GetCommand();
                    return false;
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (SelectedIndex == 0) return true;
                    else if ((SelectedIndex == 1)) return false;
                    else if (SelectedIndex == 2) Program.GetCommand(); ;
                }


            }

            return FINAL;
        }


        /// <summary>
        /// Bu da sadece bilgilendirme icin biseyler gosteriyor
        /// orn: lutfen isim doldurunuz, yasakli kelime iceriyor.
        /// </summary>
        /// <param name="Aciklama"></param>
        /// <param name="SayfaAdi"></param>
        /// <param name="HataKodu"></param>
        public static void ShowNotification(string Aciklama = "Aciklama_Default", string SayfaAdi = "Page_Name_Default", string HataKodu = "Err_Cde_Dflt")
        {
            string[] Ops = { "Tamam!","Main_Menu" };
            int SelectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(SayfaAdi);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("UYARI:");
                Console.ResetColor();
                Console.WriteLine(Aciklama.ToUpper());
                Console.ResetColor();


                for (int i = 0; i < Ops.Length; i++)
                {
                    if (i == SelectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("=> " + Ops[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(Ops[i]);
                    }

                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("Kod:" + HataKodu);
                Console.ResetColor();

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow && SelectedIndex > 0)
                {
                    SelectedIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && SelectedIndex < Ops.Length - 1)
                {
                    SelectedIndex++;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow && SelectedIndex == 0)
                {
                    SelectedIndex = Ops.Length - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && SelectedIndex == Ops.Length - 1)
                {
                    SelectedIndex = 0;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Program.GetCommand();
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (SelectedIndex == 1) Program.GetCommand();
                    else if (SelectedIndex == 0) break;
                }


            }

        }




        
        private static readonly char[] SpecialChars = "!@#$%^&*()".ToCharArray();
        /// <summary>
        /// System.IO da file name bu charlari iceremiyo ondan ufak check kismi bu herhangi bi file isleminde
        /// </summary>
        /// <param name="Word"></param>
        /// <returns></returns>
        public static bool ContainsForbittenLetters(string Word)
        {
            int indexOf = Word.IndexOfAny(SpecialChars);
            if (indexOf == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
