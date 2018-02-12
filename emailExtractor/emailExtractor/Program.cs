using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvContents();




        }



        public static void CsvContents()
        {
            string fileContents = "";

            //bool for checking file type is right
            bool fileTypeCorrect = false;

            Console.WriteLine("This is an email extractor");
            Console.WriteLine("Instructions as followed: ");
            Console.Write("1) Enter the location (file directory / path) of file you wish to extract emails from\n" +
                "2) Enter the name you wish the output file to be called\n+" +
                "From here this program will save the file to your desktop\n" + 
                "Simple Huh? Now let's try that\n\n");

            while (fileTypeCorrect == false)
            {
                Console.WriteLine("Please enter a valid .csv file location: ");
                string fileLocation = Console.ReadLine();

                //check if the file location is correct
                if (File.Exists(fileLocation))
                {
                    string extension = Path.GetExtension(fileLocation);

                    if (extension.Equals(".csv"))
                    {
                        fileContents = File.ReadAllText(fileLocation);
                        fileTypeCorrect = true;
                    }
                }

            }

            Console.WriteLine("Reading Content....");
            Console.WriteLine("Extracting....");


            List<string> emailList = EmailExtraction(fileContents);

            SaveOutput(emailList);

        }


        public static List<string> EmailExtraction(String _csvContents)
        {
            List<string> emailReturnList = new List<string>();

            //create a new array for splitting the string
            string[] _stringExtractor = _csvContents.Split(',');

            //look through each string in extracted string to see if any contain @ symbol
            foreach (string a in _stringExtractor)
            {
                if (a.Contains("@"))
                {
                    emailReturnList.Add(a);
                }
            }
            //return the email list
            return emailReturnList;
        }


        /// <summary>
        /// Takes string list as param
        /// saves output to location specified by user
        /// </summary>
        /// <param name="_emailList"></param>
        public static void SaveOutput(List<string> _emailList)
        {
            Console.WriteLine("Extracted emails.");

            
            Console.WriteLine("Please enter a name for the file: ");
            string fileName = Console.ReadLine();


            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fullFileName = Path.Combine(desktopFolder, fileName + ".txt");
            var fs = new FileStream(fullFileName, FileMode.Create);

            string dataasstring = "data"; //your data

            int counter = 0;

            foreach (string a in _emailList)
            {
                dataasstring = a; //your data
                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
                fs.Write(info, 0, info.Length);
                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                fs.Write(newline, 0, newline.Length);
                counter++;
            }

            

            fs.Close();

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

        }
    }
}
