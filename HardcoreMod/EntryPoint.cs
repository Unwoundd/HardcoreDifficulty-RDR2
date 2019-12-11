[assembly: Rage.Attributes.Plugin
(
    name: "Hardcore Mod",
    Author = "Unwound", 
    Description = "Ride on NPC's horses and vehicles", 
    EntryPoint = "HardcoreMod.EntryPoint.OnEntry", 
    ExitPoint = "HardcoreMod.EntryPoint.OnUnloading"
)]

namespace HardcoreMod
{
    using Rage;

    internal static class EntryPoint
    {
        /// <summary>
        /// Called when the plugin has been loaded.
        /// </summary>
        private static void OnEntry()
        {
            HardcoreMod.Start();

            while (true)
            {
                HardcoreMod.Process();
                GameFiber.Yield();
            }
        }

        /// <summary>
        /// Called when the plugin is being unloaded.
        /// </summary>
        /// <param name="isTerminating">if set to <c>true</c>, the plugin is being terminated, and calls to GameFiber methods are illegal.</param>
        private static void OnUnloading(bool isTerminating)
        {
            HardcoreMod.End();
        }
    }
}