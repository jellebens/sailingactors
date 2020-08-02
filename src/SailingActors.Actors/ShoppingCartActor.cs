using Dapr.Actors;
using Dapr.Actors.Runtime;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SailingActors.Actors;
using SailingActors.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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


        protected override async Task OnActivateAsync()
        {
            _Logger.LogInformation($"Activating actor with Id {this.Id}");

            await Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync()
        {
            _Logger.LogInformation($"Deactivating actor with Id {this.Id}");

            return Task.CompletedTask;
        }

        public async Task Add(long productId, string name, int quantity)
        {
            _Logger.LogInformation("Adding Item to cart");

            List<ShoppingcartItem> items;

            var getResult = await StateManager.TryGetStateAsync<List<ShoppingcartItem>>("items");

            if (getResult.HasValue)
            {
                items = getResult.Value;
            }
            else
            {
                items = new List<ShoppingcartItem>();
            }

            ShoppingcartItem item = items.SingleOrDefault(i => i.ProductId == productId);
            
            if(item != null)
            {
                _Logger.LogInformation($"Item allready in cart adding {quantity}");
                item.Quantity += quantity;
            }
            else
            {
                _Logger.LogInformation($"Item not in cart adding to cart");
                items.Add(new ShoppingcartItem()
                {
                    Name = name,
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await StateManager.SetStateAsync("items", items);

            _Logger.LogInformation("Finished adding Item to cart");
        }
    }
}