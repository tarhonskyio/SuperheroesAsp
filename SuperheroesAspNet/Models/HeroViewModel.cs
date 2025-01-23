namespace SuperheroesAspNet.Models.Superheroes
{
    public class HeroViewModel
    {
        public int Id { get; set; }
        public string SuperheroName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int? HeightCm { get; set; }
        public int? WeightKg { get; set; }
        public List<int> SelectedPowers { get; set; } = new List<int>();
    }
}