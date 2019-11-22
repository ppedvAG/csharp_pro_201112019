using ppedv.FixDox.Model;
using ppedv.FixDox.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.FixDox.Logic
{
    public class Core : IDisposable
    {
        public IRepository Repository { get; private set; }

        public Core(IRepository repository)
        {
            this.Repository = repository;
        }


        public IEnumerable<Device> GetDemoData()
        {
            var d1 = new Device() { Name = "Fischer Price Kassettenrecorder mit Microphon" };
            d1.Medium = new Medium() { Typ = MediumTyp.Magnetband, Bezeichnung = "Kassette", Länge = 45 };
            yield return d1;

            var d2 = new Device() { Name = "Sony Laserdisk Player 2000" };
            d2.Medium = new Medium() { Typ = MediumTyp.Laserdisk, Bezeichnung = "Laserdisk", Länge = 64 };

            yield return d2;
        }

        public void CreateDemoDaten()
        {
            GetDemoData().ToList().ForEach(x => Repository.Add(x));

            Repository.SaveChanges();
        }

        public void Dispose() => Repository?.Dispose();
    }
}
