namespace POKEMONAPP.Models
{
    public class Pokemon
        {
            public string Name { get; set; }
            public List<string> Moves { get; set; }
            public List<string> Abilities { get; set; }
        }


    public class PokemonList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<PokemonResult> Results { get; set; }
    }
    public class PokemonResult
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
