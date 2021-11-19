namespace curso.api.Busines.Entities
{
    public class Course
    {
        public int Code { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public int UserCode { get; set; }
        public virtual User User { get; set; }
    }
}
