namespace Fme.Library.Repositories
{
    public class DataField
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public DataField(string name, int ordinal)
        {
            this.Name = name;
            this.Ordinal = ordinal;
        }

        public string GetQualifiedName()
        {
            if (Name.Contains(" "))
                return "[" + Name + "]";           
          return Name;                
        }
    }
}
