using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Text.Json;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace DgDerleme
{
    internal class Kisi_Ekle_Sayfa
    {
        public DogumGunuInfoClass DOGUMGUNU;
        static int selectedItemIndexCommands = 0;
        public bool OynamaYapildi = false;

        public Kisi_Ekle_Sayfa()
        {
            DOGUMGUNU = new DogumGunuInfoClass()
            {
                Date = DateTime.Today,
                Entry_Date = DateTime.Now,
                Two_Week_Warning = false
            };
            OynamaYapildi = false;
            ShowInfo();
        }


        public void ShowInfo()
        {
            while(true) {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Kisi_Ekle_Sayfa");
                Console.ResetColor();

                Console.WriteLine("");
                if (selectedItemIndexCommands == 0) Console.BackgroundColor = ConsoleColor.Blue;
                else Console.ResetColor();
                Console.WriteLine("Date: " + DOGUMGUNU.Date.ToShortDateString() + "(" + DOGUMGUNU.Date.ToLongDateString() + ")");

                if (selectedItemIndexCommands == 1) Console.BackgroundColor = ConsoleColor.Blue;
                else Console.ResetColor();
                Console.WriteLine("Name: " + DOGUMGUNU.Name);

                if (selectedItemIndexCommands == 2) Console.BackgroundColor = ConsoleColor.Blue;
                else Console.ResetColor();
                Console.WriteLine("Notes: " + DOGUMGUNU.Notes);

                if (selectedItemIndexCommands == 3) Console.BackgroundColor = ConsoleColor.Blue;
                else Console.ResetColor();
                Console.WriteLine("TwoWeekWarning: " + DOGUMGUNU.Two_Week_Warning);

                if (selectedItemIndexCommands == 4) Console.BackgroundColor = ConsoleColor.Blue;
                else Console.ResetColor();
                Console.WriteLine("EntryDate: " + DOGUMGUNU.Entry_Date );

                if (selectedItemIndexCommands == 5) Console.BackgroundColor = ConsoleColor.Blue;
                else Console.ResetColor();
                Console.WriteLine("Image_Path: " + DOGUMGUNU.Image_Path);

                Console.WriteLine();

                if (selectedItemIndexCommands == 6) Console.BackgroundColor = ConsoleColor.Green;
                else Console.ResetColor();
                Console.WriteLine("ONAYLA");

                if (selectedItemIndexCommands == 7) Console.BackgroundColor = ConsoleColor.Red;
                else Console.ResetColor();
                Console.WriteLine("Cikis..");


                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("Duzenlemek icin (ENTER)");
                Console.WriteLine("Navigasyon icin (YUKARI/ASAGI)");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow && selectedItemIndexCommands > 0)
                {
                    selectedItemIndexCommands--;
                    //Console.Beep();
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && selectedItemIndexCommands < 7)
                {
                    selectedItemIndexCommands++;
                    //Console.Beep();
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && selectedItemIndexCommands == 7)
                {
                    selectedItemIndexCommands = 0;
                    //Console.Beep();
                }else if(keyInfo.Key == ConsoleKey.UpArrow && selectedItemIndexCommands == 0)
                {
                    selectedItemIndexCommands = 7;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if(selectedItemIndexCommands == 0)
                    {
                        //Get Date
                        GetDate("Lutfen Yeni Tarihi Giriniz..");
                    }
                    if (selectedItemIndexCommands == 1)
                    {
                        //Get Name
                        GetName();
                    }
                    if (selectedItemIndexCommands == 2)
                    {
                        //Notes
                        GetNotes();
                    }
                    if(selectedItemIndexCommands == 3)
                    {
                        //two week uyari
                        DOGUMGUNU.Two_Week_Warning = !DOGUMGUNU.Two_Week_Warning;
                    }
                    if(selectedItemIndexCommands == 5)
                    {
                        GetImgPath();
                    }
                    if (selectedItemIndexCommands == 7)
                    {
                        if (InputClass.OnayYesNo("Dikkat Degisiklik Yaptiniz. Savlemeden Cikmak Istediginize emin Misiniz?"))
                        {
                            Program.GetCommand();
                            break;
                        }
                    }
                    if (selectedItemIndexCommands == 6)
                    {
                        if (string.IsNullOrEmpty(DOGUMGUNU.Name) || string.IsNullOrWhiteSpace(DOGUMGUNU.Name))
                        {
                            InputClass.ShowNotification("Lutfen Isim Kismini Doldurunuz..", this.GetType().Name, "K_E_S A1");
                            ShowInfo();
                            return;
                        }

                        if (InputClass.ContainsForbittenLetters(DOGUMGUNU.Name))
                        {
                            InputClass.ShowNotification("Isim Yasakli Harfler Iceriyor.. =>!@#$%^&*()", this.GetType().Name, "K_E_S A2");
                            ShowInfo();
                            return;
                        }

                        

                        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dates");
                        Directory.CreateDirectory(folderPath);

                        string filePath = Path.Combine(folderPath, $"{DOGUMGUNU.Name}.json");
                        
                        if(File.Exists(filePath))
                        {
                            InputClass.ShowNotification("Dikkat Bu Isimde Bir Dogum Gunu Zaten Kayitli..", GetType().Name, "K_E_S A3");
                            ShowInfo();
                            return;
                        }


                        string json = System.Text.Json.JsonSerializer.Serialize(DOGUMGUNU, new JsonSerializerOptions { WriteIndented = true });

                        // Save the JSON to the file
                        File.WriteAllText(filePath, json);

                        InputClass.ShowNotification("Dogum Gunu (Json) basasriyla kaydedildi.", this.GetType().Name, "Null");
                        Program.GetCommand();
                        break;
                    }
                }
            }
        }

        private void GetImgPath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select an image file";
            fileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedFilePath = fileDialog.FileName;
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Selected file: " + selectedFilePath);
                Console.ResetColor();
                DOGUMGUNU.Image_Path = selectedFilePath;
                OynamaYapildi = true;
            }
            else
            {
                Console.WriteLine("No file selected.");
                MessageBox.Show("Hata");
            }

        }

        private void GetNotes()
        {
            bool Responded = false;
            OynamaYapildi = true;
            while (!Responded)
            {
                string NAME = InputClass.GetStringFromUser("Lutfen Notlarinizi Giriniz..", "Not_Secme", "Kisi_Ekle_Sayfa");
                if (NAME != null)
                {
                    if (NAME != "%20")
                    {
                        Responded = true;
                        DOGUMGUNU.Notes = NAME;
                    }
                    else
                    {
                        Responded = true;
                        break;
                    }
                }
                else
                {
                    Responded = false;
                }
            }
        }

        #region DateKismi
        enum DateComponent
        {
            Day,
            Month,
            Year
        }
        public void GetDate(string Comment)
        {
            DateTime currentDate = DateTime.Today;
            DateTime selectedDate = currentDate;

            ConsoleKeyInfo keyInfo;

            int selectedDay = selectedDate.Day;
            int selectedMonth = selectedDate.Month;
            int selectedYear = selectedDate.Year;

            DateComponent selectedComponent = DateComponent.Day;

            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Kisi_Ekle_Sayfa");
                Console.ResetColor();
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Date_Secme");
                Console.ResetColor();
                Console.WriteLine("kardes ok kullanarak tarih sec:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=> / <= Gun/Ay/Yil");
                Console.WriteLine("Onayla (Enter)");
                Console.WriteLine("Iptal (BackSpace)");
                Console.ResetColor();
                Console.WriteLine();
                DateTime TempselectedDate = new DateTime(selectedYear, selectedMonth, selectedDay);
                string Main = $"Secilen Tarih: {selectedDay}-{selectedMonth}-{selectedYear}";
                string formattedDate = GetFormattedDate(selectedComponent, selectedDay, selectedMonth, selectedYear, TempselectedDate);
                //Console.WriteLine(formattedDate);

                DisplayCalendar(selectedMonth, selectedYear, selectedComponent, selectedDay);
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        //Amk Sol -1
                        if (selectedComponent == DateComponent.Day)
                            selectedComponent = DateComponent.Year;
                        else if (selectedComponent == DateComponent.Month)
                            selectedComponent = DateComponent.Day;
                        else if (selectedComponent == DateComponent.Year)
                            selectedComponent = DateComponent.Month;
                        break;
                    case ConsoleKey.RightArrow:
                        //sag -1
                        if (selectedComponent == DateComponent.Day)
                            selectedComponent = DateComponent.Month;
                        else if (selectedComponent == DateComponent.Month)
                            selectedComponent = DateComponent.Year;
                        else if (selectedComponent == DateComponent.Year)
                            selectedComponent = DateComponent.Day;
                        break;
                    case ConsoleKey.UpArrow:
                        //arttir 
                        if (selectedComponent == DateComponent.Day)
                            selectedDay++;
                        else if (selectedComponent == DateComponent.Month)
                            selectedMonth++;
                        else if (selectedComponent == DateComponent.Year)
                            selectedYear++;
                        break;
                    case ConsoleKey.DownArrow:
                        //azalt 
                        if (selectedComponent == DateComponent.Day)
                            selectedDay--;
                        else if (selectedComponent == DateComponent.Month && selectedMonth > 1)
                            selectedMonth--;
                        else if (selectedComponent == DateComponent.Year)
                            selectedYear--;
                        break;
                    case ConsoleKey.Backspace:
                        ShowInfo();
                        break;

                }

                //buralari bilmeon (Ssagol chat gpt)
                selectedDay = Math.Max(1, Math.Min(DateTime.DaysInMonth(selectedYear, selectedMonth), selectedDay));
                selectedMonth = Math.Max(1, Math.Min(12, selectedMonth));
                selectedYear = Math.Max(1, selectedYear);

            } while (keyInfo.Key != ConsoleKey.Enter);

            selectedDate = new DateTime(selectedYear, selectedMonth, selectedDay);
            //Console.WriteLine("\nSelected date: " + selectedDate.ToShortDateString());
            DOGUMGUNU.Date = selectedDate;
            OynamaYapildi = true;
            //Console.ReadKey();
        }

        static void DisplayCalendar(int month, int year, DateComponent selectedComponent, int selectedDay)
        {
            Console.WriteLine();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DayOfWeek startDayOfWeek = firstDayOfMonth.DayOfWeek;

            int startDay = ((int)startDayOfWeek - 1 + 7) % 7;

            Console.WriteLine("\n Mon Tue Wed Thu Fri Sat Sun");

            Console.Write(new string(' ', 4 * startDay));

            for (int day = 1; day <= daysInMonth; day++)
            {
                if (day == selectedDay && selectedComponent == DateComponent.Day)
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                if ((day + startDay) % 7 == 0 && day != selectedDay)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                if(day == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
                    Console.ForegroundColor = ConsoleColor.Magenta;
                

                Console.Write($"{day,4}");
                Console.ResetColor();

                if ((day + startDay) % 7 == 0 || day == daysInMonth)
                    Console.WriteLine();
            }
        }

        string GetFormattedDate(DateComponent selectedComponent, int selectedDay, int selectedMonth, int selectedYear, DateTime selectedDate)
        {
            string dateStr = "Secilen Tarih ==> ";
            Console.Write(dateStr);
            switch (selectedComponent)
            {
                case DateComponent.Day:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    dateStr += $"{selectedDay}";
                    Console.Write($"{selectedDay} ({selectedDate.ToString("ddd")})");
                    Console.ResetColor();
                    dateStr += $"-{selectedMonth}-{selectedYear}";
                    Console.Write($"-{selectedMonth}-{selectedYear}");

                    Console.Write($" ({selectedDate.ToLongDateString()})");
                    break;
                case DateComponent.Month:
                    dateStr += $"{selectedDay}-";
                    Console.Write($"{selectedDay}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    dateStr += $"{selectedMonth}";
                    Console.Write($"-{selectedMonth} ({selectedDate.ToString("MMMM")})-");
                    Console.ResetColor();
                    dateStr += $"{selectedYear}";
                    Console.Write($"{selectedYear}");

                    Console.Write($" ({selectedDate.ToLongDateString()})");

                    break;
                case DateComponent.Year:
                    dateStr += $"{selectedDay}-{selectedMonth}-";
                    Console.Write($"{selectedDay}-{selectedMonth}-");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    dateStr += $"{selectedYear}";
                    Console.Write($"{selectedYear}");
                    Console.ResetColor();

                    Console.Write($" ({selectedDate.ToLongDateString()})");

                    break;
            }

            return dateStr;
        }
        #endregion

        void GetName()
        {
            bool Responded= false;
            OynamaYapildi = true;
            while (!Responded)
            {
                string NAME = InputClass.GetStringFromUser("Lutfen Isminizi Giriniz..", "Name_Secme", "Kisi_Ekle_Sayfa");
                if (NAME != null)
                {
                    if (NAME != "%20")
                    {
                        Responded = true;
                        DOGUMGUNU.Name = NAME;
                    }
                    else
                    {
                        Responded = true;
                        break;
                    }
                }
                else
                {
                    Responded = false;
                }
            }
        }
    }
}
