using ppedv.FixDox.Logic;
using ppedv.FixDox.Model;
using ppedv.FixDox.Model.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ppedv.FixDox.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** FixDox v0.1 ***");

            for (int i = 0; i < 100; i++)
            {
                Task.Run(() => { var sing = Singelton.Instance; });
            }


            ConcurrentBag<string> dinge = new ConcurrentBag<string>();
            for (int i = 0; i < 100; i++)
            {
                Task.Run(() => dinge.Add(Guid.NewGuid().ToString()));
                Task.Run(() =>
                {
                    //  if (i > 5 && i % 2 == 0)
                    //      dinge.TryTake(out "Hallo");
                });
            }


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

            using (var core = new Core(repo))
            {

                //var aa = core.Repository.GetAll<Device>().Count();
                if (core.Repository.Query<Device>().Count() == 0)
                    core.CreateDemoDaten();

                foreach (var d in core.Repository.GetAll<Device>())
                {
                    Console.WriteLine($"{d.Name} {d.Medium?.Bezeichnung} {d.Medium?.Typ} {d.Medium?.Länge} Minuten");
                }
            }
            //            foreach (var item in coreeg-lrk .GetDemoData())
            //            {
            //                Console.WriteLine(item.Name);
            //            }

            Console.WriteLine("Ende");
            Console.ReadLine();

            var sing2 = Singelton.Instance;
        }

    }


    class Singelton
    {
        private static object syncObj = new object();
        private static Singelton _instance = null;
        public static Singelton Instance
        {
            get
            {
                lock (syncObj)
                {
                    if (_instance == null)
                        _instance = new Singelton();

                    return _instance;
                }
            }
        }

        private Singelton()
        {
            Console.WriteLine("HALLO ICH BIN DER CTOR");
        }
    }
}
