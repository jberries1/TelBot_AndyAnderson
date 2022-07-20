using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Text;
using System;


var botClient = new TelegramBotClient("5479851852:AAHe0BgvKEYQJaehJi2qA-aUnmMen_tn8d8");

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions

{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);
var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;



    var chatId = message.Chat.Id;
    if (message.Text == ("Только!") || message.Text == "ТОЛЬКО!")
    {
        Message message1 = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "TOP-DOG!",
            cancellationToken: cancellationToken);

    }


    string a = message.Text;
    string b = "Бластер";
    string c = "Кратос";
    using (var stream = System.IO.File.OpenRead("D:/AllProject/TelBot/AndyBot/Video/Blaster.mp4"))
    using (var stream1 = System.IO.File.OpenRead("D:/AllProject/TelBot/AndyBot/Video/Cratos1.mp4"))

        if (message.Text != null)
        {
            bool CheckMessage = a.IndexOf(b, StringComparison.OrdinalIgnoreCase) >= 0;
            if (CheckMessage)
            {
                Message message3 = await botClient.SendVideoAsync(
                 chatId: chatId,
                 video: stream,
                 supportsStreaming: true,
                 cancellationToken: cancellationToken);
            }
            bool CheckMessage1 = a.IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0;
            if (CheckMessage1)
            {
                Message message2 = await botClient.SendVideoAsync(
                chatId: chatId,
                video: stream1,
                supportsStreaming: true,
                cancellationToken: cancellationToken);
            }

        }
    if (message.Text == "Луи" || message.Text == "луи")
    {
        Message message1 = await botClient.SendStickerAsync(
        chatId: chatId,
        sticker: "https://tlgrm.ru/_/stickers/f93/7be/f937be74-d603-3fd2-8f00-d43a83f28adb/3.webp",
        cancellationToken: cancellationToken);

    }
    if(message.Text == "/help")
    {
        Message message1 = await botClient.SendTextMessageAsync(
           chatId: chatId,
           text: "Список команд: ",
           cancellationToken: cancellationToken);
    }
    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
}


Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}