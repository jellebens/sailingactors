using Dapr.Actors;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SailingActors.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SailingActors.Actors
{
    public class HealthActor : Actor, IHealthActor
    {
        private readonly ILogger<HealthActor> _Logger;

        public HealthActor(ActorService actorService, ActorId actorId) : base(actorService, actorId)
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

            _Logger = loggerFactory.CreateLogger<HealthActor>();
        }

        public Task Live()
        {
            return Task.CompletedTask;
        }

        public Task Ready()
        {
            return Task.CompletedTask;
        }
    }
}
