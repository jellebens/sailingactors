using Dapr.Actors;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SailingActors.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalilingActors.Actors
{
    public class ShoppingCartActor : Actor, IShoppingCart
    {
        private readonly ILogger<ShoppingCartActor> _Logger;

        public ShoppingCartActor(ActorService actorService, ActorId actorId, IActorStateManager actorStateManager = null) : base(actorService, actorId, actorStateManager)
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

        public Task Add(long productId, string name, int quantity)
        {
            return Task.CompletedTask;
        }
    }
}
