namespace DiscordBot.Services
{
    public class SingleHitService
    {
        public static int GetHit(int HitRoll, int HitOne, string Character, int WeaponDamageBonus, int WeaponPenetration)
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

			int hitLocation = 0;

			int diceResult = HitOne;

			switch (firstHitLocation)
            {
                // Head hit first
                case 1:
                    hitLocation = characterArmourHead;
                break;

                // Arm hit first
                case 2:
                    hitLocation = characterArmourArms;
                break;

                // Body hit first
                case 3:
                    hitLocation = characterArmourBody;
                break;

                // Leg hit first
                case 4:
                    hitLocation = characterArmourLegs;
                break;
            }

			int WeaponDamageTotal = 0;

            int endArmourValue;
            
            endArmourValue = hitLocation - weaponPenetration;

            if (endArmourValue < 0)
            {
                endArmourValue = 0;
            }

            WeaponDamageTotal = (diceResult + weaponDamageBonus) - (endArmourValue + characterToughnessBonus); 

            if (WeaponDamageTotal < 0)
            {
                return 0;
            }

			return WeaponDamageTotal;
        }
    }
}