
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

namespace DiscordBot.Services
{
    public class YeetService
    {
        public Task RespondtoUserMessageAsync(SocketMessage messageParam)
        {
            
            Console.WriteLine(messageParam.Author);
            Console.WriteLine(messageParam.Channel);

            // if (messageParam.Author.ToString() == "Gheydragon#9467")
            // {
            //     messageParam.Channel.SendMessageAsync("ok boomer");
            // }

            return Task.CompletedTask;
        }
    }
}