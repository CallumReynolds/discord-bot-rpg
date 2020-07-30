using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using DiscordBot.Services;
using System.Net.Http;
using DiscordBot.Characters;

namespace DiscordBot.Services
{
    public class TwoHitsService
    {
        public static int GetHits(int HitRoll, int HitOne, int HitTwo, string Character, int WeaponDamageBonus, int WeaponPenetration)
        {
            int[] myInts = WhichCharacterService.CharacterStats(Character);

            // Weapon Stats
            int weaponPenetration = WeaponPenetration;
            int weaponDamageBonus = WeaponDamageBonus;

            // Character Stats
            int characterToughnessBonus = myInts[0];
            int characterArmourHead = myInts[1];
            int characterArmourBody = myInts[2];
            int characterArmourArms = myInts[3];
            int characterArmourLegs = myInts[4];

            //await ReplyAsync(Character + " toughness bonus is: " + characterToughnessBonus);

			HitRoll = ToHitService.Reverse(HitRoll);

            int firstHitLocation = ToHitService.FirstHitLocation(HitRoll);

			int[] hitLocation = new int[2];

			int[] diceResults = new int[2];

			diceResults[0] = HitOne;

			diceResults[1] = HitTwo;

			switch (firstHitLocation)
            {
                // Head hit first
                case 1:
                    switch (2)
                    {
                       case 2:
                        hitLocation[0] = characterArmourHead;
                        hitLocation[1] = characterArmourHead;
                        break;
                    }
                break;

                // Arm hit first
                case 2:
                    switch (2)
                    {
                        case 2:
                        hitLocation[0] = characterArmourArms;
                        hitLocation[1] = characterArmourArms;
                        break;
                    }
                break;

                // Body hit first
                case 3:
                    switch (2)
                    {
                        case 2:
                        hitLocation[0] = characterArmourBody;
                        hitLocation[1] = characterArmourBody;
                        break;
                    }
                break;

                // Leg hit first
                case 4:
                    switch (2)
                    {
                        case 2:
                        hitLocation[0] = characterArmourLegs;
                        hitLocation[1] = characterArmourLegs;
                        break;
                    }
                break;
            }

			int WeaponDamageTotal = 0;

            int endArmourValue;
            //
            for (int i = 0; i < diceResults.Length; i++)
            {
                endArmourValue = hitLocation[i] - weaponPenetration;
                if(endArmourValue < 0)
                {
                    endArmourValue = 0;
                }
                WeaponDamageTotal += (diceResults[i] + weaponDamageBonus) - (endArmourValue + characterToughnessBonus); 
            }

            if (WeaponDamageTotal < 0)
            {
                return 0;
            }

			return WeaponDamageTotal;
        }
    }
}