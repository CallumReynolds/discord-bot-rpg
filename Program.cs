using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using DiscordBot.Services;

// the program (initialization and command handler)
// the modules (handle commands)
// the services (persistent storage, pure functions, data manipulation)

public class Program
{
    public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
    public async Task MainAsync()
    {
        using (var services = ConfigureServices())
            {
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                client.MessageReceived += YeetAsync;

                // Tokens should be considered secret data and never hard-coded.
                // We can read from the environment variable to avoid hardcoding.
                await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("token"));
                await client.StartAsync();

                // Here we initialize the logic required to register our commands.
                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                Console.WriteLine("yeet");

                await Task.Delay(-1);
            }
    }

    private Task YeetAsync(SocketMessage messageParam)
    {
        
        Console.WriteLine(messageParam.Author);
        Console.WriteLine(messageParam.Channel);

        if (messageParam.Author.ToString() == "Gheydragon#9467")
        {
            messageParam.Channel.SendMessageAsync("ok boomer");
        }

        if (messageParam.Author.ToString() == "Gheyface#6401")
        {
            messageParam.Channel.SendMessageAsync("Whatever Jordan...");
        }

        if (messageParam.Author.ToString() == "Gheykaiser#1328")
        {
            messageParam.Channel.SendMessageAsync("Cool story Bailey, needs more dragons!");
        }
        
        if (messageParam.Author.ToString() == "Traenacha#3393")
        {
            messageParam.Channel.SendMessageAsync("What. A. Cutay hehe");
        }

        return Task.CompletedTask;
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log.ToString() + " yeet");

        return Task.CompletedTask;
    }

    private ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            // Base
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandlingService>()
            .AddSingleton<HttpClient>()
            .AddSingleton<PictureService>()
            // Logging
            .AddLogging()
            .AddSingleton<LogService>()
            // Extra
            //.AddSingleton(_config)
            // Add additional services here...
            .BuildServiceProvider();
    }

    private IConfiguration BuildConfig()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config.json")
            .Build();
    }
}


public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _services;

    // Retrieve client and CommandService instance via ctor
    public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
    {
        _commands = commands;
        _client = client;
        _services = services;
    }

    public async Task InitializeAsync()
	{
		// Pass the service provider to the second parameter of
		// AddModulesAsync to inject dependencies to all modules 
		// that may require them.
		await _commands.AddModulesAsync(
			assembly: Assembly.GetEntryAssembly(), 
			services: _services);
		_client.MessageReceived += HandleCommandAsync;
	}

    public async Task InstallCommandsAsync()
    {
        // Hook the MessageReceived event into our command handler
        _client.MessageReceived += HandleCommandAsync;

        // Here we discover all of the command modules in the entry 
        // assembly and load them. Starting from Discord.NET 2.0, a
        // service provider is required to be passed into the
        // module registration method to inject the 
        // required dependencies.
        //
        // If you do not use Dependency Injection, pass null.
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), 
                                        services: _services);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        // Don't process the command if it was a system message
        var message = messageParam as SocketUserMessage;
        // Create a WebSocket-based command context based on the message
        var context = new SocketCommandContext(_client, message);

        if (message == null) return;

        // Create a number to track where the prefix ends and the command begins
        int argPos = 0;

        // Determine if the message is a command based on the prefix and make sure no bots trigger commands
        if (!(message.HasCharPrefix('!', ref argPos) || 
            message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        

        // Execute the command with the command context we just
        // created, along with the service provider for precondition checks.
        await _commands.ExecuteAsync(
            context: context, 
            argPos: argPos,
            services: _services);
    }
}

