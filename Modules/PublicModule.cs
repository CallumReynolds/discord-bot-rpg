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

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!

namespace DiscordBot.Modules
{
	public class PublicModule : ModuleBase<SocketCommandContext>
	{
		public struct TypeReaderValue
		{
			public int[] ButtStompa { get; }

			public TypeReaderValue(int[] buttStompa)
			{
				ButtStompa = buttStompa;
			}
		}

		public string[] ButtStompa = new string[5] {8.ToString(),4.ToString(),4.ToString(),4.ToString(),4.ToString()};
		public PictureService PictureService { get; set; }

        private PropertyInfo[] _PropertyInfos = null;

		[Command("atest")]
		public async Task YeetAsync()
		{
            if(_PropertyInfos == null)
                _PropertyInfos = typeof(Stats).GetProperties(); //GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var item in _PropertyInfos)
            {
                var value = item.GetValue(this, null) ?? "(null)";
                sb.AppendLine(item.Name);
            }

            //return sb.ToString();

            await ReplyAsync(sb.ToString());
			//await ReplyAsync(ButtStompa[0]);
		}

		[Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync()
            => ReplyAsync("pong!");
		
		[Command("cat")]
        public async Task CatAsync()
        {
            // Get a stream containing an image of a cat
            var stream = await PictureService.GetCatPictureAsync();
            // Streams must be seeked to their beginning before being uploaded!
            stream.Seek(0, SeekOrigin.Begin);
            await Context.Channel.SendFileAsync(stream, "cat.png");
        }

		[Command("test")]
		public async Task TestAsync()
		{
			using (HttpClient client = new HttpClient())
			{
				var response = client.GetAsync("http://localhost:5000/api/values");
				if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var result = response.Result.Content.ReadAsStringAsync();
					await ReplyAsync(result.Result);
				}
				else
				{
					await ReplyAsync("Oh noes!");
				}
			}
			
		}


		[Command("info")]
		public async Task InfoAsync()
		{
			var msg = $@"Hi {Context.User}!";
			await ReplyAsync(msg);
		}

		// Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }

		// ~say hello world -> hello world
		[Command("say")]
		[Summary("Echoes a message.")]
		public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
			=> ReplyAsync(echo);

			

		//ReplyAsync is a method on ModuleBase

		[Command("hitlocation")]
		[Summary("The number to hit.")]
		public async Task HitLocationAsync(
	 		[Summary("The number to hit.")] 
	 		int hit)
		{
			hit = ToHitService.Reverse(hit);

			if (hit >=1 && hit <= 10)
            {
                await ReplyAsync("The head is hit!");
            }
            else if (hit >= 11 && hit <= 20)
            {
                await ReplyAsync("The right arm is hit!");
            }
            else if (hit >= 21 && hit <= 30)
            {
                await ReplyAsync("The left arm is hit!");
            }
            else if (hit >= 31 && hit <= 70)
            {
                await ReplyAsync("The body is hit!");
            }
            else if (hit >= 71 && hit <= 85)
            {
                await ReplyAsync("The right leg is hit!");
            }
            else{
                await ReplyAsync("The left leg is hit!");
            }
		}	
		
		// Use Polymorphism here for calculating the different types of hits
		[Command("2hits")]
		[Summary("2 Hits")]
		public async Task HitCalculation(int HitRoll, int HitOne, int HitTwo, string Character, int WeaponDamageBonus, int WeaponPenetration)
		{
            int dmg = TwoHitsService.GetHits(HitRoll, HitOne, HitTwo, Character, WeaponDamageBonus, WeaponPenetration);

            await ReplyAsync(Character + " takes " + dmg + " damage!");

            if (dmg == 0)
            {
                await ReplyAsync(QuoteService.LowDmgQuote());
            }
		}

        [Command("hit")]
        [Summary("Calculates a single hit + damage.")]
        public async Task HitCalculation(int HitRoll, int HitOne, string Character, int WeaponDamageBonus, int WeaponPenetration)
        {
            int dmg = SingleHitService.GetHit(HitRoll, HitOne, Character, WeaponDamageBonus, WeaponPenetration);

            await ReplyAsync(Character + " takes " + dmg + " damage!");

            if (dmg == 0)
            {
                await ReplyAsync(QuoteService.LowDmgQuote());
            }
        }

        [Command("6hits")]
        [Summary("Calculates 6 hits + damage.")]
        public async Task HitCalculation(int HitRoll, int HitOne, int HitTwo, int HitThree, int HitFour, int HitFive, int HitSix, string Character, int WeaponDamageBonus, int WeaponPenetration)
        {
            int dmg = SixHitsService.GetHits(HitRoll, HitOne, HitTwo, HitThree, HitFour, HitFive, HitSix, Character, WeaponDamageBonus, WeaponPenetration);

            await ReplyAsync(Character + " takes " + dmg + " damage!");

            if (dmg == 0)
            {
                await ReplyAsync(QuoteService.LowDmgQuote());
            }
        }
	}

	// public class InfoModule : ModuleBase<SocketCommandContext>
	// {
	// 		// ~say hello world -> hello world
	// 	[Command("say")]
	// 	[Summary("Echoes a message.")]
	// 	public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
	// 		=> ReplyAsync(echo);
			
	// 	// ReplyAsync is a method on ModuleBase
	// }

	// // Create a module with the 'sample' prefix
	// [Group("sample")]
	// public class SampleModule : ModuleBase<SocketCommandContext>
	// {
	// 	// ~sample square 20 -> 400
	// 	[Command("square")]
	// 	[Summary("Squares a number.")]
	// 	public async Task SquareAsync(
	// 		[Summary("The number to square.")] 
	// 		int num)
	// 	{
	// 		// We can also access the channel from the Command Context.
	// 		await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
	// 	}

	// 	// ~sample userinfo --> foxbot#0282
	// 	// ~sample userinfo @Khionu --> Khionu#8708
	// 	// ~sample userinfo Khionu#8708 --> Khionu#8708
	// 	// ~sample userinfo Khionu --> Khionu#8708
	// 	// ~sample userinfo 96642168176807936 --> Khionu#8708
	// 	// ~sample whois 96642168176807936 --> Khionu#8708
	// 	[Command("userinfo")]
	// 	[Summary
	// 	("Returns info about the current user, or the user parameter, if one passed.")]
	// 	[Alias("user", "whois")]
	// 	public async Task UserInfoAsync(
	// 		[Summary("The (optional) user to get info from")]
	// 		SocketUser user = null)
	// 	{
	// 		var userInfo = user ?? Context.Client.CurrentUser;
	// 		await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
	// 	}
	// }
}