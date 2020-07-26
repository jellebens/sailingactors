using Dapr.Actors;
using System;

namespace SailingActors.Shared
{
    public interface IShoppngCart: IActor
    {
        public void Submit();
    }
}
