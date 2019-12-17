[assembly: Rage.Attributes.Plugin(
    name: "Hardcore Difficulty",
    Author = "Unwound and Vesdii", 
    Description = "Game modifications to increase difficulty", 
    EntryPoint = "HardcoreDiff.EntryPoint.OnEntry", 
    ExitPoint = "HardcoreDiff.EntryPoint.OnUnloading"
)]

namespace HardcoreDiff {
    using Rage;

    internal static class EntryPoint {
        private static void OnEntry() {
            HardcoreDiff.Start();

            while (true) {
                HardcoreDiff.Process();
                GameFiber.Yield();
            }
        }

        private static void OnUnloading(bool isTerminating) {
            HardcoreDiff.End();
        }
    }
}