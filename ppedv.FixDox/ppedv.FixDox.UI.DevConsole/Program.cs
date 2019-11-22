using ppedv.FixDox.Logic;
using ppedv.FixDox.Model;
using System;
using System.Linq;

namespace ppedv.FixDox.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** FixDox v0.1 ***");

            var core = new Core();

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
