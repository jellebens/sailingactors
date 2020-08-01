using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SailingActors.Api.Contracts;
using SailingActors.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SailingActors.Api.Controllers
{
    [ApiController]
    public class AddToCartController: ControllerBase
    {
        private readonly ILogger<AddToCartController> _Logger;
        private readonly ICartIdGenerator _IdGenerator;

        public AddToCartController(ILogger<AddToCartController> logger, ICartIdGenerator generator)
        {
            _Logger = logger;
            _IdGenerator = generator;
        }


        [HttpPost("/cart/{id}/item")]
        public async Task<IActionResult> Item(string id, AddItemModel addItem) {
            _Logger.LogInformation($"starting to add item to cart {id}");

            Activity activity = new Activity("cart.put.item");
            activity.Start();

            if (string.IsNullOrEmpty(id)) {
                id = _IdGenerator.New();
            }

            string ActorType = "ShoppingCart";

            ActorId actorId = new ActorId(id);

            IShoppingCart cart = ActorProxy.Create<IShoppingCart>(actorId, ActorType);

            await cart.Add(addItem.Id, addItem.Name, addItem.Quantity);

            activity.Stop();

            return Ok();
        }
    }
}
