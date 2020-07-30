namespace DiscordBot.Services
{
    public class SixHitsService
    {
        public static int GetHits(int HitRoll, int HitOne, int HitTwo, int HitThree, int HitFour, int HitFive, int HitSix, string Character, int WeaponDamageBonus, int WeaponPenetration)
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

			int[] hitLocation = new int[6];

			int[] diceResults = new int[6];

			diceResults[0] = HitOne;

			diceResults[1] = HitTwo;
            
			diceResults[2] = HitThree;

			diceResults[3] = HitFour;
            
			diceResults[4] = HitFive;

			diceResults[5] = HitSix;

			switch (firstHitLocation)
            {
                // Head hit first
                case 1:
                    switch (2)
                    {
                       case 2:
                        hitLocation[0] = characterArmourHead;
                        hitLocation[1] = characterArmourHead;
                        hitLocation[2] = characterArmourArms;
                        hitLocation[3] = characterArmourBody;
                        hitLocation[4] = characterArmourArms;
                        hitLocation[5] = characterArmourBody;
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
                        hitLocation[2] = characterArmourBody;
                        hitLocation[3] = characterArmourHead;
                        hitLocation[4] = characterArmourBody;
                        hitLocation[5] = characterArmourArms;
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
                        hitLocation[2] = characterArmourArms;
                        hitLocation[3] = characterArmourHead;
                        hitLocation[4] = characterArmourArms;
                        hitLocation[5] = characterArmourBody;
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
                        hitLocation[2] = characterArmourBody;
                        hitLocation[3] = characterArmourArms;
                        hitLocation[4] = characterArmourHead;
                        hitLocation[5] = characterArmourBody;
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