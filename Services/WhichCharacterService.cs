using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System;
using System.IO;
using DiscordBot.Services;
using System.Net.Http;
using DiscordBot.Characters;

namespace DiscordBot.Services
{
    public class WhichCharacterService
    {
        // Determines which character gets hit and returns that characters stats.
        public static int[] CharacterStats(string character)
        {
            int[] myInts;

            if (character == nameof(Stats.ButtStompa))
            {
                myInts = Array.ConvertAll(Stats.ButtStompa, int.Parse);

                return myInts;
            } 
            else if (character == nameof(Stats.Charity))
            {
                myInts = Array.ConvertAll(Stats.Charity, int.Parse);

                return myInts; 
            }
            else if (character == nameof(Stats.Alek))
            {
                myInts = Array.ConvertAll(Stats.Alek, int.Parse);

                return myInts; 
            }
            else if (character == nameof(Stats.Zaiss))
            {
                myInts = Array.ConvertAll(Stats.Zaiss, int.Parse);

                return myInts; 
            }
            else if (character == nameof(Stats.LadyAsh))
            {
                myInts = Array.ConvertAll(Stats.LadyAsh, int.Parse);

                return myInts; 
            }
            else if (character == nameof(Stats.BattleServitor))
            {
                myInts = Array.ConvertAll(Stats.BattleServitor, int.Parse);

                return myInts; 
            }

            return null;
        }
    }
}