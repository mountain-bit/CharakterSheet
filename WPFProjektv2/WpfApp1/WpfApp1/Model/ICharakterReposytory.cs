using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    internal interface ICharakterReposytory
    {
        //CRUD Charaketer
        void AddCharakter(Charakter charakter);

        void UpdateCharakter(Charakter charakter);

        void DeleteCharakter(Charakter charakter);

        Charakter GetCharakterById(int id);

       IQueryable<Charakter> GetAllCharakters();
       IQueryable<Charakter> GetCharaktersByPlayerId(int playerId);

        //CRUD Player
        void AddPlayer(Player player);

        void UpdatePlayer(Player player);

        void DeletePlayer(Player player);

        Player GetPlayerById(int id);

       IQueryable<Player> GetAllPlayers();

        //CRUD Roll

        void AddRoll(Roll roll);

        void UpdateRoll(Roll roll);

        void DeleteRoll(Roll roll);

        Roll GetRollById(int id);

       IQueryable<Roll> GetAllRolls();
       IQueryable<Roll> GetRollsByCharakterId(int charakterId);


    }
}
