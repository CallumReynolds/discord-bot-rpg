using System.Collections.Generic;
using System;

namespace DiscordBot.Services
{
    public class QuoteService
    {
        List<string> movieQuote;
        private static List<string> lowDamageQuote = new List<String> 
        {
            "So much damage NOT dealt senpai!~",
            "You suck UwU",
            "THAT's A LOT OF DAMGE LOL",
            "Oof soooo baaaaad hehe :3",
            "Git gud xD",
            "GG no re scrub"
        };
        
        
        public string RandomQuote()
        {

        movieQuote.Add("So much damage senpai!~");
        movieQuote.Add("UwU");
        movieQuote.Add("THAT's A LOT OF DAMGE");
        movieQuote.Add("Oof soooo gooood hehe :3");

        Random randNr = new Random();

        int aRandomPos = randNr.Next(1, movieQuote.Count);

        string quote = movieQuote[aRandomPos];

        return quote;

        }

        public static string LowDmgQuote()
        {

        Random randNr = new Random();

        int aRandomPos = randNr.Next(1, lowDamageQuote.Count);

        string quote = lowDamageQuote[aRandomPos];

        return quote;

        }
    }
}