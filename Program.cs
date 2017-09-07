using System;                  
using System.Threading.Tasks;
using Confuser.Core;           
using Confuser.Core.Project;   
using System.IO;
using System.Xml;
using Rpx;

namespace VirtualConfuserEx
{
    class ProtectFull
    {
        private int size;

        public ProtectFull(int _size)
        {
            size = _size;
        }

        public async Task<bool> ProtectFile()
        {
            for(var i = 0; i < size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[[[ASAMA]]] {i + 1} [[[ASAMA]]]");     
                Console.ForegroundColor = ConsoleColor.White;
                new ProtectConfuser().ProtectFile().GetAwaiter().GetResult();
                new ProtectRPX().ProtectFile().GetAwaiter().GetResult();
            }

            return true;
        }
    }


    class ProtectRPX
    {
        static ProtectRPX() { }

        public async Task<bool> ProtectFile()
        {
            var fileName = "packme";
            RpxInterface.RunCommand(fileName);

            return true;  
        }     
    }

    class ProtectConfuser
    {
        static ProtectConfuser() { } 


        public async Task<bool> ProtectFile()
        {
            ConfuserProject confuserProject = new ConfuserProject();
            ConfuserParameters confuserParameters = new ConfuserParameters();
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load("Dependencies\\VirtualConfuser.Project.dll");
                confuserProject.Load(xmlDocument);
                confuserProject.BaseDirectory = Directory.GetCurrentDirectory();      
                confuserProject.OutputDirectory = Directory.GetCurrentDirectory();
            }
            catch (Exception e)
            {
                //bazi insanlar kadar bombos
            }
            
            confuserParameters.Project = confuserProject;

            int num = Confuser.CLI.Program.RunProject(confuserParameters);

            return (num == 0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "VirtualConfuserEx | Rexy";

            if (!File.Exists("packme"))
            {
                Console.WriteLine("Dosya isminiz \"packme\"'in mevcut oldugundan emin olun");
                Console.ReadKey();
                return;
            }
                  
            Console.Write("Pack sayisi ?= ");
            var a = int.Parse(Console.ReadLine());

            new ProtectFull(a).ProtectFile().GetAwaiter().GetResult();

            Console.Clear();

            Console.Title = "VirtualConfuserEx | BASARILI";

            Console.WriteLine("TAMAMLANDI !!!");

            Console.ReadKey();
        }
    }
}  
