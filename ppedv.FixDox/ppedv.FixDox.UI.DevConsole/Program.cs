using ppedv.FixDox.Logic;
using ppedv.FixDox.Model;
using ppedv.FixDox.Model.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace ppedv.FixDox.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** FixDox v0.1 ***");

  

            object o = new object();
            Type t = o.GetType();
            object o2 = Activator.CreateInstance(t);

            var dataRepoFilePath = @"C:\Users\ar2\source\repos\ppedvAG\csharp_pro_201112019\ppedv.FixDox\ppedv.FixDox.Data.EF\bin\Debug\netstandard2.1\ppedv.FixDox.Data.EF.dll";
            var ass = Assembly.LoadFrom(dataRepoFilePath);
            //foreach (var item in ass.GetTypes())
            //{
            //    Console.WriteLine(item.Name);
            //    foreach (var p in item.GetRuntimeProperties())
            //    {
            //        Console.WriteLine($"\t{p.Name}");
            //    }
            //}

            var typMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetTypeInfo()
                                                                 .ImplementedInterfaces
                                                                 .Contains(typeof(IRepository)));

            var repo = Activator.CreateInstance(typMitRepo) as IRepository;

            var core = new Core(repo);
            //var aa = core.Repository.GetAll<Device>().Count();
            if (core.Repository.Query<Device>().Count() == 0)
                core.CreateDemoDaten();

            foreach (var d in core.Repository.GetAll<Device>())
            {
                Console.WriteLine($"{d.Name} {d.Medium?.Bezeichnung} {d.Medium?.Typ} {d.Medium?.Länge} Minuten");
            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
