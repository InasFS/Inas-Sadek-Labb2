using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inas_Sadek_Labb2
{
    internal class Pirate
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int YearStarted { get; set; }
        public string Title { get; private set; }
        public Parrot Parrot { get; set; }

        // Default constructor required for the list
        public Pirate() { }

        // Constructor with name and year

        public Pirate(string name, int yearStarted)
        {
            Name = name;
            YearStarted = yearStarted;
            CalculateRank();
            SetTitle();
        }

        // Constructor with name, year and parrot
        public Pirate(string name, int yearStarted, Parrot parrot)
        {
            Name = name;
            YearStarted = yearStarted;
            Parrot = parrot;
            CalculateRank();
            SetTitle();
        }

        private void CalculateRank()
        {
            // Calculate rank based on years of service (from 1735)
            Rank = 1735 - YearStarted;
            if (Rank < 1) Rank = 1;
        }

        private void SetTitle()
        {
            if (Rank < 5)
                Title = "Deckhand";
            else if (Rank < 10)
                Title = "Boatswain";
            else if (Rank < 15)
                Title = "First Mate";
            else
                Title = "Captain";
        }

        public override string ToString()
        {
            if (Parrot != null)
                return $"{Name} ({Title}) - Parrot: {Parrot.Name}";
            return $"{Name} ({Title})";
        }
    }

}




   

