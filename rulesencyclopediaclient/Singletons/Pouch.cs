using rulesencyclopediaclient.Models;

// Adapted from https://csharpindepth.com/articles/singleton
// fully lazy instantiation. Den bliver udløst første gang der er en reference til det statiske medlem af den 
// indeholdte klasse i Instance, og den bliver kun udført en gang pr Appdomæne hvilket gør at det kun er en tråd
// ad gangen der kan køre den. Disse to ting gør den lazy og threadsafe.
namespace rulesencyclopediaclient.Pouch
{
    public sealed class Pouch
    {
        private Pouch()
        {

        }

        public static Pouch Instance { get { return get.instance; } }

        private class get
        {
            static get()
            {
            }
            internal static readonly Pouch instance = new Pouch();
        }
        public string token { get; set; }
        public UserDTO user { get; set; }

        public readonly string apiAddress = "localhost";
        public readonly string portNr = "44378";
    }
}
