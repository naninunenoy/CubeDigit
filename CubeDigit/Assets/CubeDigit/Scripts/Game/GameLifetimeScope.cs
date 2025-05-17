using UnityEngine;
using VContainer;
using VContainer.Unity;
using VitalRouter.VContainer;

namespace CubeDigit.Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] EntryPoint entryPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterVitalRouter( routing =>
            {
                routing.Map<CubeColorPresenter>();
            });
            builder.RegisterComponent(entryPoint);
        }
    }
}
