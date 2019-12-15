[assembly: Rage.Attributes.Plugin(
    name: "Hardcore Mod",
    Author = "Unwound", 
    Description = "Game modifications to increase difficulty", 
    EntryPoint = "HardcoreMod.EntryPoint.OnEntry", 
    ExitPoint = "HardcoreMod.EntryPoint.OnUnloading"
)]

namespace HardcoreMod
{
    using Rage;

    internal static class EntryPoint {
        private static void OnEntry() {
            HardcoreMod.Start();

            while (true) {
                HardcoreMod.Process();
                GameFiber.Yield();
            }
        }

        private static void OnUnloading(bool isTerminating) {
            HardcoreMod.End();
        }
    }
}