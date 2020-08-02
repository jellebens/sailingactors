using Dapr.Actors;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SailingActors.Actors;
using SailingActors.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingActors.Actors
{
    public class ShoppingCartActor : Actor, IShoppingCart
    {
        private readonly ILogger<ShoppingCartActor> _Logger;

        public ShoppingCartActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole(config => {
                        config.Format = ConsoleLoggerFormat.Systemd;
                        config.TimestampFormat = "[HH:mm:ss] ";
                    });
            });

            _Logger = loggerFactory.CreateLogger<ShoppingCartActor>();
        }

        public async Task Add(long productId, string name, int quantity)
        {
            _Logger.LogInformation("Adding Item to cart");

            await SaveStateAsync();

            _Logger.LogInformation("Finished adding Item to cart");
        }
    }
}