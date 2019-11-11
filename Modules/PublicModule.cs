using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System;
using System.IO;
using DiscordBot.Services;
using System.Net.Http;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!

namespace DiscordBot.Modules
{
	public class PublicModule : ModuleBase<SocketCommandContext>
	{
		public PictureService PictureService { get; set; }

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
					await ReplyAsync("You fucked up, Callum.");
				}
			}
			
		}


		[Command("info")]
		public async Task InfoAsync()
		{
			var msg = $@"Hi {Context.User}! (Tay is sexy hehe)";
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