using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
   public  class CharakterViewModel:BaseViewModel
    {
        private CharakterReposytory charakterReposytory;
        public Charakter Charakter { get; set; }
        private string _playerName;
        public string PlayerName { 
            get { return _playerName; } 
            set {

                _playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            } }

        public string CharakterName
        {
            get { return this.Charakter.Name; }
            set
            {
                this.Charakter.Name = value;
                OnPropertyChanged(nameof(CharakterName));
            }
        }

        public string CharakterRole
        {
            get { return this.Charakter.Role; }
            set
            {
                this.Charakter.Role = value;
                OnPropertyChanged(nameof(CharakterRole));
            }
        }

        public string CharakterDescription
        {
            get { return this.Charakter.Description; }
            set
            {
                this.Charakter.Description = value;
                OnPropertyChanged(nameof(CharakterDescription));
            }
        }

        public ObservableCollection<Stat> CharakterStats
        {
            get
            {
                return this.Charakter.Stats;
            }
        }

        public Stat SelectedStat { get; set; }

        public Skill SelectedSkill { get; set; }


        public ObservableCollection<Skill> CharakterSkills
        {
            get
            {
                return this.Charakter.Skills;
            }
        }

        public int NewStatValue { get; set; }

        public int NewSkillValue { get; set; }

        public string NewSkillName { get; set; }


        public ObservableCollection<Roll> CharakterRolls { get; set; } 

        public int CharakterHealth { get { 
            return this.Charakter.Health;
            }
            set
            {
                this.Charakter.Health = value;
                if (this.Charakter.Health > this.Charakter.MaxHealth)
                    this.Charakter.Health = this.Charakter.MaxHealth;
                else if (this.Charakter.Health < 0)
                    this.Charakter.Health = 0;
                OnPropertyChanged(nameof(CharakterHealth));
            } 
        }

        public int CharakterMaxHealth
        {
            get
            {
                return this.Charakter.MaxHealth;
            }
        }

        public int CharakterHumanity
        {
            get
            {
                return this.Charakter.Humanity;
            }
            set
            {
                this.Charakter.Humanity = value;
                if (this.Charakter.Humanity > 100)
                    this.Charakter.Humanity = 100;
                else if (this.Charakter.Humanity < 0)
                    this.Charakter.Humanity = 0;
                OnPropertyChanged(nameof(CharakterHumanity));
            }
        }

        public int CharakterReputation
        {
            get
            {
                return this.Charakter.Reputation;
            }
            set
            {
                this.Charakter.Reputation = value;
                OnPropertyChanged(nameof(CharakterReputation));
            }
        }

        public int CharakterExperience
        {
            get
            {
                return this.Charakter.Experience;
            }
            set
            {
                this.Charakter.Experience = value;
                OnPropertyChanged(nameof(CharakterExperience));
            }
        }

        public int k6Count { get; set; }



        public CharakterViewModel(Charakter charakter, CharakterReposytory charakterReposytory)
        {
            this.Charakter = charakter;
            this.charakterReposytory = charakterReposytory;
            this.PlayerName =  charakterReposytory.GetPlayerById(charakter.PlayerId).Name;

            if(charakter.Rolls?.OrderBy(s => s.Id).ToList()!=null)
            this.CharakterRolls = new ObservableCollection<Roll>(charakter.Rolls.OrderBy(s => s.Id).ToList());
            else
                this.CharakterRolls = new ObservableCollection<Roll>();

            ChangeStatCommand = new RelayCommand(ChangeStat, ()=> SelectedStat!=null);
            ChangeSkillCommand = new RelayCommand(ChangeSkill, () => SelectedSkill != null && NewSkillValue > 0);
            AddSkillCommand = new RelayCommand(AddSkill, () => !string.IsNullOrEmpty(NewSkillName) && SelectedStat != null );
            SaveChangesCommand = new RelayCommand(SaveChanges, ()=> true);
            RollCommand = new RelayCommand(SkillRoll, () => SelectedSkill != null);
            DamegeRollCommand = new RelayCommand(DamegeRoll, () =>  k6Count!=0);
            LostHumenityRollCommand = new RelayCommand(LostHumenityRoll, () => k6Count != 0);

            RemoveSkillCommand = new RelayCommand(RemoveSkill, () => SelectedSkill != null);
        }

        public void ChangeStat()
        {

                SelectedStat.Value = NewStatValue;
                this.Charakter.Stats = new ObservableCollection<Stat>(this.Charakter.Stats.OrderByDescending(s => s.Value));
            if(SelectedStat.Name == "Body")
            {
                this.CharakterHealth = SelectedStat.Value * 10;
                OnPropertyChanged(nameof(CharakterHealth));
                OnPropertyChanged(nameof(CharakterMaxHealth));
            }

            OnPropertyChanged(nameof(CharakterStats));
            
        }

        public void ChangeSkill()
        {
                SelectedSkill.Value = NewSkillValue;
          
            this.Charakter.Skills = new ObservableCollection<Skill>(this.Charakter.Skills.OrderByDescending(s => s.Value));
            OnPropertyChanged(nameof(CharakterSkills));
        }

        public void AddSkill()
        {

                this.Charakter.Skills.Add(new Skill { Name = NewSkillName, BaseStat = SelectedStat.Name, Value =0 });
                this.Charakter.Skills = new ObservableCollection<Skill>(this.Charakter.Skills.OrderByDescending(s => s.Value));
                OnPropertyChanged(nameof(CharakterSkills));
            
        }

        public void SaveChanges()
        {
            Charakter.createStatsDBDescription();
            Charakter.createSkillsDBDescription();
            if (Charakter.Id == 0)
            {
                charakterReposytory.AddCharakter(Charakter);
            }
            else
                charakterReposytory.UpdateCharakter(Charakter);
        }

        public void SkillRoll()
        {
            Roll roll = new Roll();
            roll.CharakterId = this.Charakter.Id;
            roll.SkillName = SelectedSkill.Name;
            //skill + stats value
            roll.BaseValue = SelectedSkill.Value +( this.Charakter.Stats.FirstOrDefault(s => s.Name == SelectedSkill.BaseStat)?.Value ?? 0);
            roll.Modifier = 0;
            roll.Dificulty = 13; // Default difficulty, can be changed later

            roll.makeRoll();


            charakterReposytory.AddRoll(roll);
            this.CharakterRolls.Add(roll);
            OnPropertyChanged(nameof(CharakterRolls));
        }


        public void DamegeRoll()
        {
            Roll roll = new Roll();
            roll.CharakterId = this.Charakter.Id;
            roll.SkillName = "Damage";
            roll.makeRoll(k6Count);

            charakterReposytory.AddRoll(roll);
            this.CharakterRolls.Add(roll);
            OnPropertyChanged(nameof(CharakterRolls));
            this.CharakterHealth -= roll.RolledValue;
        }

        public void LostHumenityRoll()
        {
            Roll roll = new Roll();
            roll.CharakterId = this.Charakter.Id;
            roll.SkillName = "Humanity lost";
            roll.makeRoll(k6Count);
            charakterReposytory.AddRoll(roll);
            this.CharakterRolls.Add(roll);
            OnPropertyChanged(nameof(CharakterRolls));
            this.CharakterHumanity -= roll.RolledValue;
        }

        public void RemoveSkill()
        {
                this.Charakter.Skills.Remove(SelectedSkill);
                OnPropertyChanged(nameof(CharakterSkills));
            
        }


        public ICommand ChangeStatCommand { get; set; }
        public ICommand ChangeSkillCommand { get; set; }

        public ICommand AddSkillCommand { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        public ICommand RollCommand { get; set; }

        public ICommand DamegeRollCommand {  get; set; }

        public ICommand LostHumenityRollCommand { get; set; }

        public ICommand RemoveSkillCommand { get; set; }



    }
}
