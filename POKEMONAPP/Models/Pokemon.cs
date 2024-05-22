namespace POKEMONAPP.Models
{
    public class PokemonList
    {
        public List<PokemonResult> Results { get; set; }
    }
    public class Pokemon
        {
            public string Name { get; set; }
            public List<string> Moves { get; set; }
            public List<string> Abilities { get; set; }
        }

    
}
