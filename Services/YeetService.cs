
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

            if (messageParam.Author.ToString() == "KrakenMacc#9653")
            {
                messageParam.Channel.SendMessageAsync("okay");
            }

            return Task.CompletedTask;
        }
    }
}