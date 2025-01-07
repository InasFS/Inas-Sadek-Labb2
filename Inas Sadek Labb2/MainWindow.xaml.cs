using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inas_Sadek_Labb2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Pirate> tortugaPirates = new List<Pirate>();
        private List<Pirate> shipCrew = new List<Pirate>();
        private bool isCrewFullyRecruited = false;  // Flag to track if crew is fully recruited

        // Constructor

        public MainWindow()
        {
            InitializeComponent();
            InitializePirates();
            InitializeRankComboBox();
            UpdatePirateList();

        }
        // Initialize pirates with example data
        private void InitializePirates()
        {
            var polly = new Parrot("Polly");
            var bronko = new Parrot("Bronko");
            var charlie = new Parrot("Charlie");

            tortugaPirates.Add(new Pirate("Jack Sparrow", 1720, polly));
            tortugaPirates.Add(new Pirate("William Turner", 1725));
            tortugaPirates.Add(new Pirate("Elizabeth Swann", 1730, bronko));
            tortugaPirates.Add(new Pirate("Hector Barbossa", 1715, charlie));
            tortugaPirates.Add(new Pirate("Joshamee Gibbs", 1722));
            tortugaPirates.Add(new Pirate("Marty Smith", 1728));
            tortugaPirates.Add(new Pirate("Cotton Pete", 1719));
            tortugaPirates.Add(new Pirate("Anamaria Jones", 1726));
            tortugaPirates.Add(new Pirate("Pintel Davis", 1721));
            tortugaPirates.Add(new Pirate("Ragetti Rogers", 1723));
            tortugaPirates.Add(new Pirate("Bootstrap Bill", 1710));
            tortugaPirates.Add(new Pirate("Tia Dalma", 1718));
        }

        // Update Pirate ListBox
        private void UpdatePirateList()
        {
            PirateListBox.ItemsSource = null;
            PirateListBox.Items.Clear();

            foreach (var pirate in tortugaPirates)
            {
                PirateListBox.Items.Add(pirate);
            }
        }
        private void InitializeRankComboBox()
        {
            RankComboBox.Items.Add("Deckhand");
            RankComboBox.Items.Add("Boatswain");
            RankComboBox.Items.Add("First Mate");
            RankComboBox.Items.Add("Captain");
            RankComboBox.SelectedIndex = 0;
        }
        // Update Crew ListBox based on selected rank
        private void UpdateCrewList()
        {
            if (isCrewFullyRecruited) return; // Stop if the crew is fully recruited

            string currentRank = RankComboBox.SelectedItem as string;
            if (currentRank == null) return;

            CrewListBox.ItemsSource = null;
            CrewListBox.Items.Clear();

            foreach (var pirate in shipCrew)
            {
                if (pirate.Title == currentRank)
                {
                    CrewListBox.Items.Add(pirate);
                }
            }
        }

        // Update status label for crew count
        private void UpdateStatusDisplay()
        {
            if (isCrewFullyRecruited) return; // Stop if the crew is fully recruited

            string currentRank = RankComboBox.SelectedItem as string;
            if (currentRank == null) return;

            int currentCount = 0;
            foreach (var pirate in shipCrew)
            {
                if (pirate.Title == currentRank)
                {
                    currentCount++;
                }
            }

            int requiredCount = GetCrewRequirement(currentRank);
            StatusLabel.Content = $"Current {currentRank} Count: {currentCount}/{requiredCount}";

            if (currentCount >= requiredCount)
            {
                MessageBox.Show($"{currentRank} positions are filled!");
                MoveToNextRank();
            }
        }

        // Move to the next rank after the current rank is full
        private void MoveToNextRank()
        {
            if (isCrewFullyRecruited) return; // Prevent further processing

            int currentIndex = RankComboBox.SelectedIndex;
            if (currentIndex < RankComboBox.Items.Count - 1)
            {
                RankComboBox.SelectedIndex = currentIndex + 1;
                MessageBox.Show($"Moving to next rank: {RankComboBox.SelectedItem}");
            }
            else
            {
                MessageBox.Show("All positions are filled! The ship is ready to sail!");
                isCrewFullyRecruited = true;
                DisplayFinalCrew();  // Show the final crew list
            }

            UpdateStatusDisplay();
        }

        // Get the required crew size for each rank
        public int GetCrewRequirement(string rank)
        {
            if (rank == "Deckhand") return 10;
            else if (rank == "Boatswain") return 3;
            else if (rank == "First Mate") return 1;
            else if (rank == "Captain") return 1;
            else return 0;
        }

        // Recruitment button for individual pirate
        private void RecruitButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCrewFullyRecruited) return; // Prevent further recruitment if crew is full

            var selectedPirate = PirateListBox.SelectedItem as Pirate;
            if (selectedPirate == null)
            {
                MessageBox.Show("Please select a pirate to recruit!");
                return;
            }

            string currentRank = RankComboBox.SelectedItem as string;
            if (!IsQualifiedForRank(selectedPirate, currentRank))
            {
                MessageBox.Show($"This pirate is not qualified for the {currentRank} position!");
                return;
            }

            int currentCount = shipCrew.Count(pirate => pirate.Title == currentRank);
            int requiredCount = GetCrewRequirement(currentRank);

            if (currentCount >= requiredCount)
            {
                MessageBox.Show($"{currentRank} positions are filled!");
                return;
            }

            shipCrew.Add(selectedPirate);
            tortugaPirates.Remove(selectedPirate);
            UpdateAllLists();
            UpdateStatusDisplay(); // Ensure the status is updated after recruiting
        }

        // Recruitment button for the entire crew based on selected rank
        private void RecruitCrewButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCrewFullyRecruited) return; // Prevent further recruitment if crew is full

            var selectedPirate = PirateListBox.SelectedItem as Pirate;
            if (selectedPirate == null)
            {
                MessageBox.Show("Please select a pirate to recruit!");
                return;
            }

            string currentRank = RankComboBox.SelectedItem as string;
            if (!IsQualifiedForRank(selectedPirate, currentRank))
            {
                MessageBox.Show($"This pirate is not qualified for the {currentRank} position!");
                return;
            }

            int currentCount = shipCrew.Count(pirate => pirate.Title == currentRank);
            int requiredCount = GetCrewRequirement(currentRank);

            if (currentCount >= requiredCount)
            {
                MessageBox.Show($"{currentRank} positions are filled!");
                return;
            }

            shipCrew.Add(selectedPirate);
            tortugaPirates.Remove(selectedPirate);
            UpdateAllLists();
            UpdateStatusDisplay(); // Ensure the status is updated after recruiting
        }

        // Check if a pirate is qualified for the selected rank
        private bool IsQualifiedForRank(Pirate pirate, string rank)
        {
            if (rank == "Deckhand") return true;
            else if (rank == "Boatswain") return pirate.Rank >= 5;
            else if (rank == "First Mate") return pirate.Rank >= 10;
            else if (rank == "Captain") return pirate.Rank >= 15;
            else return false;
        }

        // Update both pirate and crew lists after recruitment
        private void UpdateAllLists()
        {
            PirateListBox.Items.Clear();
            CrewListBox.Items.Clear();

            foreach (var pirate in tortugaPirates)
            {
                PirateListBox.Items.Add(pirate);
            }

            UpdateCrewList();
        }

        // Update rank selection and check for completed recruitment
        private void RankComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isCrewFullyRecruited)  // Only update if crew is not fully recruited
            {
                UpdateCrewList();
                UpdateStatusDisplay();
            }
        }

        // Event triggered when the window is loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateStatusDisplay();
        }

        // Event triggered when a pirate with a parrot is double-clicked
        private void PirateListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedPirate = PirateListBox.SelectedItem as Pirate;
            if (selectedPirate?.Parrot != null)
            {
                MessageBox.Show(selectedPirate.Parrot.Talk("Land i sikte"),
                    $"{selectedPirate.Parrot.Name} says:",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        // Show the final recruited crew list
        private void DisplayFinalCrew()
        {
            CrewListBox.Items.Clear();
            foreach (var pirate in shipCrew)
            {
                CrewListBox.Items.Add(pirate); // Show the complete crew with ranks
            }
        }
    }
}




   
