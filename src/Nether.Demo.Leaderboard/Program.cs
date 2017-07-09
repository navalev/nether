using Microsoft.Azure.EventHubs.Processor;
using Nether.EventHubs;
using Nether.Ingest;
using Nether.Cosmos;
using System;
using System.Threading.Tasks;

namespace Nether.Demo.Leaderboard
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine(@"   _   _      _   _               ");
            Console.WriteLine(@"  | \ | | ___| |_| |__   ___ _ __ ");
            Console.WriteLine(@"  |  \| |/ _ \ __| '_ \ / _ \ '__|");
            Console.WriteLine(@"  | |\  |  __/ |_| | | |  __/ |   ");
            Console.WriteLine(@"  |_| \_|\___|\__|_| |_|\___|_| Leaderboard");
            Console.WriteLine(@"  Nether Leaderboard ");
            Console.WriteLine();

            var app = new ProgramEx();
            app.RunAsync().Wait();
        }
    }

    public class ProgramEx
    {
        private int _infoMax = 10;

        public async Task RunAsync()
        {
            // Check that all configurations are set before continuing

            if (!Config.Check())
            {
                // Exiting due to missing configuration
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                return;
            }

            // Setup event hub listener. 
            var listenerConfig = new EventProcessorHostOptions
            {
                EventHubConnectionString = Config.Root[Config.NAH_EHLISTENER_CONNECTIONSTRING],
                EventHubPath = Config.Root[Config.NAH_EHLISTENER_EVENTHUBPATH],
                ConsumerGroupName = Config.Root[Config.NAH_EHLISTENER_CONSUMERGROUP],
                StorageConnectionString = Config.Root[Config.NAH_EHLISTENER_STORAGECONNECTIONSTRING],
                LeaseContainerName = Config.Root[Config.NAH_EHLISTENER_LEASECONTAINERNAME]
            };
            var listener = new EventHubsListener(listenerConfig, new EventProcessorOptions { MaxBatchSize = 1000, PrefetchCount = 30000 });

            // Setup Message Parser. By default we are using Nether JSON Messages
            // Setting up parser that knows how to parse those messages.
            var parser = new EventHubListenerMessageJsonParser(new ConsoleCorruptMessageHandler()) { AllowDbgEnqueuedTime = true };

            // User a builder to create routing infrastructure for messages and the pipelines
            var builder = new MessageRouterBuilder();

            var filePathAlgorithm = new DateFolderStructure(newFileOption: NewFileNameOptions.Every3Hours);

            // setup leaderboard pipeline
            var scoreSerializer = new CsvMessageFormatter("id", "type", "version", "enqueuedTimeUtc", "gameId", "userId", "score");

            builder.Pipeline("default-leaderboard")
                .HandlesMessageType("score", 1, 0)               
                .OutputTo(new DefaultLeaderboardCosmosDBOutputManager(scoreSerializer, 
                                                                      Config.Root[Config.NLB_COSMOS_DB_URL], 
                                                                      Config.Root[Config.NLB_COSMOS_DB_KEY], 
                                                                      Config.Root[Config.NLB_DB_NAME]));

            // Build all pipelines
            var router = builder.Build();

            // Attach the differeing parts of the message processor together
            var messageProcessor = new MessageProcessor<EventHubListenerMessage>(listener, parser, router, OnMessageProcessorInfoAsync);

            // The following method will never exit
            await messageProcessor.ProcessAndBlockAsync();
        }
        

        private Task OnMessageProcessorInfoAsync(MessageProcessorInformation info)
        {
            while (info.MessagesPerSecond > _infoMax)
            {
                _infoMax *= 10;
            }

            Console.WriteLine(InfoBar("Msg/s", 10, info.MessagesPerSecond, _infoMax, 80) + " Tot: " + info.TotalMessages);

            return Task.CompletedTask;
        }

        private string InfoBar(string label, int labelWidth, double value, int max, int length)
        {
            var percentageFilled = value / max;
            var v = $"({value.ToString("N2")})";
            var adjustedLength = length - labelWidth - 7 - v.Length; // remove start and end characters from bar length "Msg/s [#####        ] 9999"

            var filledChars = (int)(percentageFilled * adjustedLength);
            var unfilledChars = adjustedLength - filledChars;

            return label.PadRight(labelWidth) + "[" + new string('=', filledChars) + v + new string(' ', unfilledChars) + "] " + max.ToString().PadLeft(4);
        }
    }
}