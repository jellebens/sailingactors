using Dapr.Actors;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SailingActors.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalilingActors.Actors
{
    public class ShoppingCart : Actor, IShoppngCart
    {
        private readonly ILogger<ShoppingCart> _Logger;

        public ShoppingCart(ActorService actorService, ActorId actorId, IActorStateManager actorStateManager = null) : base(actorService, actorId, actorStateManager)
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

            _Logger = loggerFactory.CreateLogger<ShoppingCart>();
        }

        public void Submit()
        {
            
        }
    }
}
