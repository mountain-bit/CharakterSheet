using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class CharakterReposytory : ICharakterReposytory
    {
        public CharaktersBDContext context { get; set; }

        public CharakterReposytory()
        {
            context = new CharaktersBDContext();
        }
        public void AddCharakter(Charakter charakter)
        {
                charakter.createSkillsDBDescription();
            charakter.createStatsDBDescription();   
                context.Charakters.Add(charakter);
                context.SaveChanges();
            
        }

        public void AddPlayer(Player player)
        {
             
            
                context.Add(player);
                context.SaveChanges();
            


        }

        public void AddRoll(Roll roll)
        {
            
                context.Add(roll);
                context.SaveChanges();
            

        }

        public void DeleteCharakter(Charakter charakter)
        {
            
                context.Remove(charakter);
                context.SaveChanges();
            

        }

        public void DeletePlayer(Player player)
        {
           
                context.Remove(player);
                context.SaveChanges();
            

        }

        public void DeleteRoll(Roll roll)
        {
            
                context.Remove(roll);
                context.SaveChanges();
            

        }

        public IQueryable<Charakter> GetAllCharakters()
        {
             
            {
                return context.Charakters.Include(c => c.Player).Include(r=>r.Rolls).AsQueryable();
            }

        }

        public IQueryable<Player> GetAllPlayers()
        {
             
            {
                return context.Players.AsQueryable();
            }

        }

        public IQueryable<Roll> GetAllRolls()
        {
             
            {
                return context.Rolls.Include(c => c.Charakter).AsQueryable();
            }

        }

        public Charakter GetCharakterById(int id)
        {

                return context.Charakters.Include(c => c.Player).Include(r => r.Rolls).FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Charakter> GetCharaktersByPlayerId(int playerId)
        {

            IQueryable<Charakter> q = context.Charakters.Include(c => c.Player).Include(r => r.Rolls).Where(c => c.PlayerId == playerId).AsQueryable();
            if(q==null)
                return new List<Charakter>().AsQueryable();
            return q;
            

        }

        public Player GetPlayerById(int id)
        {
             
            {
                return context.Players.Include(c => c.Charaktere).FirstOrDefault(c => c.Id == id);
            }

        }

        public Roll GetRollById(int id)
        {
             
            {
                return context.Rolls.Include(c => c.Charakter).FirstOrDefault(c => c.Id == id);
            }

        }

        public IQueryable<Roll> GetRollsByCharakterId(int charakterId)
        {
             
                return context.Rolls.Include(c => c.Charakter).Where(c => c.CharakterId == charakterId).AsQueryable();

        }

        public void UpdateCharakter(Charakter charakter)
        {
             
            {
                charakter.createSkillsDBDescription();
                charakter.createStatsDBDescription();
                context.Update(charakter);
                context.SaveChanges();
            }

        }

        public void UpdatePlayer(Player player)
        {
             
            {
                context.Update(player);
                context.SaveChanges();
            }

        }

        public void UpdateRoll(Roll roll)
        {
             
            {
                context.Update(roll);
                context.SaveChanges();
            }

        }
    }
}
