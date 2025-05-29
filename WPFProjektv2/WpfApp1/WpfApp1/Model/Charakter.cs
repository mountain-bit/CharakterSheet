using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{


    public class Charakter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] // Primary key
        //cyberpunk Red
        public int Id { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; } 
        public string Name { get; set; }
        public string Role { get; set; }

        public string Description { get; set; }

        public int Reputation { get; set; }

        public int Experience { get; set; }

        public int Health { get; set; }

        public int Humanity { get; set; }

        [Required]
        public string SkillsDBDescription { get; set; } // Description of the skills database

        [Required]
        public string StatsBDescription { get; set; } // Description of the stats database

        [NotMapped]
        public ObservableCollection<Skill> Skills { get; set; } 

        [NotMapped]
        public ObservableCollection<Stat> Stats { get; set; } 

        public int MaxHealth
        {
            get
            {
                return Stats.FirstOrDefault(s => s.Name == "Body")?.Value * 10 ?? 0;
            }
        }

        public void createSkillsDBDescription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var skill in Skills)
            {
                sb.AppendLine($"{skill.Name}:{skill.BaseStat}:{skill.Value},");
            }
            SkillsDBDescription = sb.ToString();
        }

        public void createStatsDBDescription()
        {

            StringBuilder sb = new StringBuilder();
            foreach (var stat in Stats)
            {
                sb.AppendLine($"{stat},");
            }
            StatsBDescription = sb.ToString();
        }

        public void createStatsFromDBDescription()
        {

            StringBuilder sb = new StringBuilder();
            string[] statValues = StatsBDescription.Split(',');
            Stats = new ObservableCollection<Stat>();
            foreach (var stat in statValues)
            {
                string[] parts = stat.Split(':');
                if (parts.Length == 2)
                {
                    Stats.Add(new Stat(parts[0].Trim(), int.Parse(parts[1])));
                }
            }
            }

        public void createSkillsFromDBDescription()
        {
            StringBuilder sb = new StringBuilder();
            string[] skillValues = SkillsDBDescription.Split(',');
            Skills = new ObservableCollection<Skill>();
            foreach (var skill in skillValues)
            {
                string[] parts = skill.Split(':');
                if (parts.Length == 3)
                {
                    Skills.Add(new Skill { Name = parts[0].Trim(), BaseStat = parts[1], Value = int.Parse(parts[2]) });
                }
            }
        }

        public void StartCharakter()
        {
            this.createStatsFromDBDescription();
            this.createSkillsFromDBDescription();
        }

        public void StartNewChrakter()
        {
            this.Stats = new ObservableCollection<Stat>();
            this.Stats.Add(new Stat("Body", 0));
            this.Stats.Add(new Stat("Reflexes", 0));
            this.Stats.Add(new Stat("Cool", 0));
            this.Stats.Add(new Stat("Technical Ability", 0));
            this.Stats.Add(new Stat("Intelligence", 0));
            this.Stats.Add(new Stat("Luck", 0));
            this.Stats.Add(new Stat("Movement", 0));
            this.Stats.Add(new Stat("Empathy", 0));
            this.Skills = new ObservableCollection<Skill>();
            this.Skills.Add(new Skill { Name = "Athletics", BaseStat = "Reflexes", Value = 0 });
            this.Skills.Add(new Skill { Name = "Brawling", BaseStat = "Body", Value = 0 });
            this.Skills.Add(new Skill { Name = "Handgun", BaseStat = "Reflexes", Value = 0 });
            this.createStatsDBDescription();
            this.createSkillsDBDescription();

            this.Reputation = 0;
            this.Experience = 0;
            this.Health = MaxHealth;
            this.Humanity = 100; 
            this.Name = "New Character";
            this.Role = "Nomada"; // Default role
            this.Description = " ";

        }

        public Charakter()
        {
        }


        //navigation property

        public virtual Player Player { get; set; }

        public virtual ICollection<Roll> Rolls { get; set; }
    }

    

    public class Skill
    {
        public string Name { get; set; }
        public string BaseStat { get; set; }

        public int Value { get; set; }
    }

    public class Stat
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Stat(string name,int value)
        {
            Name = name;
            Value = value;
        }
        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}
