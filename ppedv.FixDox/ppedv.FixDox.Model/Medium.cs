using System.Collections.Generic;

namespace ppedv.FixDox.Model
{
    public class Medium : Entity
    {
        public string Bezeichnung { get; set; }
        public int Länge { get; set; }
        public MediumTyp Typ { get; set; }
        public virtual HashSet<Device> Devices { get; set; } = new HashSet<Device>();
        public virtual HashSet<Content> Contents { get; set; } = new HashSet<Content>();
    }

    public enum MediumTyp
    {
        Magnetband,
        Schallplatte,
        Laserdisk,
        CompactDisk,
        MiniDisk,
        Floppy,
        Flash
    }
}
