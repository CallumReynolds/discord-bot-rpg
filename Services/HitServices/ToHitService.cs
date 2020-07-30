using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public class ToHitService
    {
        public static int FirstHitLocation(int hit)
        {
            if (hit >=1 && hit <= 10)
            {
                // The head is hit!
                return 1;
            }
            else if (hit >= 11 && hit <= 20)
            {
                // The right arm is hit!
                return 2;
            }
            else if (hit >= 21 && hit <= 30)
            {
                // The left arm is hit!
                return 2;
            }
            else if (hit >= 31 && hit <= 70)
            {
                // The body is hit!
                return 3;
            }
            else if (hit >= 71 && hit <= 85)
            {
                // The right leg is hit!
                return 4;
            }
            else{
                // The left leg is hit!
                return 4;
            }
        }

        public static int Reverse(int num)
        {
            int result = 0;
            while (num > 0)
            {
                result = result * 10 + num % 10;
                num /= 10;
            }
            return result;
        }
    }
}