using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class Roll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Charakter")]
        public int CharakterId { get; set; }
        [Required]
        public string SkillName { get; set; }
        public int BaseValue { get; set; }

        public int RolledValue { get; set; }

        public int Modifier { get; set; }

        public int Dificulty { get; set; }

        public string Description { get; set; }

        // Navigation property
        public virtual Charakter Charakter { get; set; }

        public int makeRoll(int k6Count = 0)
        {
            this.Description = $"{this.SkillName} Wynik: {this.BaseValue}";
            if(this.Modifier != 0)
            {
                if (this.Modifier > 0)
                {
                    this.Description += $"+{this.Modifier}";
                }
                else
                {
                    this.Description += $"{this.Modifier}";
                }
            }
            bool k6 = false ;
            if (this.SkillName.Equals("Damage") || this.SkillName.Equals("Humanity lost"))
            {
                 k6 = true;
            }
            int wynik = 0;
            if (k6)
            {
                for (int i = 0; i < k6Count; i++)
                {
                    // Roll a k6 die
                    int roll = new Random().Next(1, 7);
                    wynik += roll;
                    Description += $"+{roll}";
                }
            }
            else
            {
                // Roll a k10 die
                int pom = new Random().Next(1, 11);
                if (pom == 10)
                {
                    Description += "+10 (Critical Success)";
                    pom = new Random().Next(1, 11);
                    Description += $"+{pom}";
                    wynik = 10 + pom; // Critical success adds an additional roll
                }else if (pom == 1)
                {
                    Description += "+1 (Critical Failure)";
                    pom = new Random().Next(1, 11);
                    Description += $"-{pom}";
                    wynik = 1 - pom; // Critical failure adds an additional roll
                }
                else
                {
                    Description += $"+{pom}";
                    wynik = pom; // Normal roll
                }
            }
            RolledValue = wynik;
            return wynik;
        }

        public Roll() { }
            

        // Method to calculate the result
        public int Result
        {
            get
            {
                return RolledValue + BaseValue + Modifier;
            }
        }

        public bool IsSuccess()
        {
            return Result >= Dificulty;
        }
    }
}
