using Telegram.Bot.Types;

namespace aaaTgBot.Handlers
{
    public static class MainHandler
    {

        public static async Task MessageProcessing(long id, Message message)
        {
            Task response = message.Text switch
            {
                //"" => 
                //_ =>  
            };

            await response;
        }

    }
}
