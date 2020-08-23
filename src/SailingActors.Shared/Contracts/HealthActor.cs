using Dapr.Actors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SailingActors.Shared.Contracts
{
    public interface IHealthActor: IActor
    {
        Task Live();

        Task Ready();
    }
}
