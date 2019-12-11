namespace HardcoreMod
{
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreMod
    {
        private static bool isDamageModifierOn = false;

        public static void Start()
        {
            Game.DisplayHelp("Welcome to hardcore mod! Version 0.1");
            GameFiber.Sleep(6000);
            Game.DisplayHelp("Hit F2 to toggle Hardcore Mode");
            GameFiber.Sleep(6000);
            Game.DisplayHelp("Good luck! - Unwound & Vesdii");
        }

        public static void Process()
        {
            ProcessInputs();
        }

        private static void ProcessInputs()
        {
            if (Game.WasKeyJustPressed(Keys.F2))
            {
                Player player = Game.LocalPlayer;

                if (isDamageModifierOn)
                {   // Toggle off
                    Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 1.0f);
                    Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 1.0f);
                    Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.0f);
                    isDamageModifierOn = false;
                    Game.DisplayHelp("Hardcore Disabled");
                }
                else
                {   // Toggle on
                    Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 3.0f);
                    Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 0.5f);
                    Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.2f);
                    isDamageModifierOn = true;
                    Game.DisplayHelp("Hardcore Enabled");
                }
            }
        }

        public static void End()
        {
            // You would free any resources here when the plugin is being unloaded.
        }
    }
}