using System.Collections.Generic;

namespace ppedv.FixDox.Model
{
    public class Content : Entity
    {
        public string Name { get; set; }
        public int Länge { get; set; }
        public virtual HashSet<Medium> Medien { get; set; } = new HashSet<Medium>();
    }
}
