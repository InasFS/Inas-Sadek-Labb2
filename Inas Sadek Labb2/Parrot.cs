using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inas_Sadek_Labb2
{
    public class Parrot
    {
        public string Name { get; set; }
        private List<string> _insults = new List<string>
        {
        "Sjörövarskabb", "Landkrabba", "Tångräka", "Klabbetok", "Sillmjölke",
        "Snäckskalle", "Kräftnjutare", "Blåsfisk", "Skrubbskädda", "Skunkröv",
        "Bläckfiskskalle", "Träskpadda", "Spiggknoge", "Kålrotspanna",
        "Knottriga kummel", "Saltstänkta krabba", "Blodiga bläckfisk",
        "Snorkråksamling", "Fiskrensätare", "Fjällrumpa", "Krabbbenstuggare",
        "Mossiga mussla", "Styvgrönsaksgegga", "Rumpnosade sjöhäst",
        "Rödkäftade sillmjölke"
        };

        public Parrot(string name)
        {
            Name = name;
        }
        public string Talk(string message)
        {
            Random random = new Random();
            string kraaa = "Kra" + new string('a', Name.Length);
            string curse = _insults[random.Next(_insults.Count)];

            string response = $"{kraaa} din {curse}! {message} ahoy!! {Name} äger";

            if (response.Length > 70)
                response += " din babbelbyxa";

            return response;
        }
    }
}



   

