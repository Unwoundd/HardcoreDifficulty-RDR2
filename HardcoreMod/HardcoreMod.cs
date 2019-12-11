namespace HardcoreMod
{
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreMod
    {
        private static bool isHardcoreOn = false;

        public static void Start()
        {
            // Announce us to the world :)
            GameFiber.Sleep(4000);
            Game.DisplayHelp("Welcome to Hardcore mod!\nVersion 0.1");
            GameFiber.Sleep(4000);
            Game.DisplayHelp("Press F2 to toggle Hardcore Mode.\nGood luck!");
       
        }

        public static void Process()
        {
            if (Game.WasKeyJustPressed(Keys.F2))
            {
                ToggleHardcore();
            }
        }

        private static void ToggleHardcore(bool forceOff = false)
        {
            Player player = Game.LocalPlayer;

            if (isHardcoreOn || forceOff)
            {
                Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 1.0f);
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 1.0f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.0f);
                Game.DisplayHelp("Hardcore Mode OFF");
                isHardcoreOn = false;
            }
            else
            {
                Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 3.0f);
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 0.6f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.2f);
                Game.DisplayHelp("Hardcore Mode ON");
                isHardcoreOn = true;
            }
        }

        public static void End()
        {
            ToggleHardcore(forceOff: true);
        }
    }
}