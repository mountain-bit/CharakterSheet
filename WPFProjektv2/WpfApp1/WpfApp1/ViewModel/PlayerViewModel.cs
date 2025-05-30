using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.Views;

namespace WpfApp1.ViewModel
{
    public class PlayerViewModel:BaseViewModel
    {
        public ObservableCollection<Player> Players { get; set; }

        public ObservableCollection<Charakter> Charakters { get; set; }

        public string SelectedCharacterInfo { get; set; }

        private Player _selectedPlayer;

        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged();
                //SelectedCharacterInfo = _selectedPlayer.Name;
                OnPropertyChanged(nameof(SelectedCharacterInfo));
                Charakters= new ObservableCollection<Charakter>(CharakterReposytory.GetCharaktersByPlayerId(_selectedPlayer.Id));
                OnPropertyChanged(nameof(Charakters));

            }
        }

        public Charakter SelectedCharakter { get; set; }

        private CharakterReposytory CharakterReposytory { get; set; } = new CharakterReposytory();

        public ICommand AddCharakterCommand { get; set; }
        public ICommand ShowSelectedCharakterCommand { get; set; }

        public ICommand RemoveSelectedCharakterCommand { get; set; }


        public void AddCharakter()
        {
            Charakter newCharakter = new Charakter();
            newCharakter.PlayerId = _selectedPlayer.Id;
            newCharakter.StartNewChrakter();
           CharakterReposytory.AddCharakter(newCharakter);
            CharakterWindow charakterWindow = new CharakterWindow();
            charakterWindow.DataContext = new CharakterViewModel(newCharakter, CharakterReposytory);
            charakterWindow.ShowDialog();

            Charakters = new ObservableCollection<Charakter>(CharakterReposytory.GetCharaktersByPlayerId(_selectedPlayer.Id));
            OnPropertyChanged(nameof(Charakters));

        }

        public void RemoveSelectedCharakter()
        {

                Charakter selectedCharakter = SelectedCharakter;
                    CharakterReposytory.DeleteCharakter(selectedCharakter);
                    Charakters.Remove(selectedCharakter);

                    OnPropertyChanged(nameof(Charakters));
        }



        public void ShoweSelectedCharakter()
        {

                CharakterWindow charakterWindow = new CharakterWindow();
            Charakter charakter = CharakterReposytory.GetCharakterById(SelectedCharakter.Id);
            charakter.StartCharakter();
            charakterWindow.DataContext = new CharakterViewModel(charakter, CharakterReposytory);

                charakterWindow.ShowDialog();

            Charakters = new ObservableCollection<Charakter>(CharakterReposytory.GetCharaktersByPlayerId(_selectedPlayer.Id));
            OnPropertyChanged(nameof(Charakters));

        }

        public PlayerViewModel()
        {
            CharakterReposytory = new CharakterReposytory();

            Players = new ObservableCollection<Player> (CharakterReposytory.GetAllPlayers());

            Charakters = new ObservableCollection<Charakter>();

            AddCharakterCommand = new RelayCommand(AddCharakter,() => _selectedPlayer != null);

            ShowSelectedCharakterCommand = new RelayCommand(ShoweSelectedCharakter, () =>_selectedPlayer != null && SelectedCharakter != null);

            RemoveSelectedCharakterCommand = new RelayCommand(RemoveSelectedCharakter, () =>_selectedPlayer != null && SelectedCharakter != null);

        }
    }
}
