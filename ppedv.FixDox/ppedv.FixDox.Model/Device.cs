namespace ppedv.FixDox.Model
{
    public class Device : Entity
    {
        public string Name { get; set; }
        public virtual Medium Medium { get; set; }
    }
}
