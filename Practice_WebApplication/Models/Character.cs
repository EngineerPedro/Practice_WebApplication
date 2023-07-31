namespace Practice_WebApplication.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string? Name { get; set; } = "Frodo";

        public int hitPoints { get; set; } = 100;

        public int strength { get; set; } = 10;

        public int defense { get; set; } = 10;

        public int intelligence { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}
