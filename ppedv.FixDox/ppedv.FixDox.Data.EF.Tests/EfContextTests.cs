using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.FixDox.Model;
using System;

namespace ppedv.FixDox.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_can_create_DB()
        {
            var conString = "Server=.\\SQLEXPRESS;Database=FixDox_Test;Trusted_Connection=true";
            var con = new EfContext(conString);

            con.Database.EnsureDeleted();

            Assert.IsTrue(con.Database.EnsureCreated());
        }

        [TestMethod]
        public void EfContext_can_CRUD_Medium()
        {
            var med = new Medium()
            {
                Bezeichnung = $"VHS_{Guid.NewGuid()}",
                Typ = MediumTyp.Magnetband,
                Länge = 120
            };
            string bez_neu = $"BETAMAX_{Guid.NewGuid()}";

            //CREATE
            using (var con = new EfContext())
            {
                con.Medien.Add(med);
                con.SaveChanges();
            }

            //READ
            using (var con = new EfContext())
            {
                var loaded = con.Medien.Find(med.Id);
                Assert.IsNotNull(loaded);
                Assert.AreEqual(med.Bezeichnung, loaded.Bezeichnung);

                //UPDATE
                loaded.Bezeichnung = bez_neu;
                con.SaveChanges();
            }

            //Check UPDATE
            using (var con = new EfContext())
            {
                var loaded = con.Medien.Find(med.Id);
                Assert.AreEqual(bez_neu, loaded.Bezeichnung);

                //DELETE
                con.Medien.Remove(loaded);
                con.SaveChanges();
            }

            //Check DELETE
            using (var con = new EfContext())
            {
                var loaded = con.Medien.Find(med.Id);
                Assert.IsNull(loaded);
            }
        }
    }
}
