using ppedv.FixDox.Model;
using ppedv.FixDox.Model.Contracts;
using System;

namespace ppedv.FixDox.Logic
{
    public class Core
    {
        public IRepository Repository { get; private set; }

        public Core(IRepository repository)
        {
            this.Repository = repository;
        }


        public void CreateDemoDaten()
        {
            var d1 = new Device() { Name = "Fischer Price Kassettenrecorder mit Microphon" };
            d1.Medium = new Medium() { Typ = MediumTyp.Magnetband, Bezeichnung = "Kassette", Länge = 45 };

            var d2 = new Device() { Name = "Sony Laserdisk Player 2000" };
            d2.Medium = new Medium() { Typ = MediumTyp.Laserdisk, Bezeichnung = "Laserdisk", Länge = 64 };

            Repository.Add(d1);
            Repository.Add(d2);
            Repository.SaveChanges();
        }
    }
}
