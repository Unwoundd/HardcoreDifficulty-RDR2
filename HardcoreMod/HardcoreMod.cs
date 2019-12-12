namespace HardcoreMod
{
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreMod
    {
        private static bool isHardcoreOn = false;

        public static void Start()
        {
            SetHardcore(true, true);
            GameFiber.Sleep(4000);
            Game.DisplayHelp("Hardcore Mod v0.1\nBy Unwound & Vesdii");
            GameFiber.Sleep(3000);
            Game.DisplayHelp("Hardcore Mode is on.\nPress F2 to toggle it.\nGood luck!");
        }

        public static void Process()
        {
            if (Game.WasKeyJustPressed(Keys.F2))
            {
                SetHardcore(!isHardcoreOn);
            }
            
            if (isHardcoreOn)
            {
                UpdateValues();
            }
        }

        private static void SetHardcore(bool state, bool hideMsg = false)
        {
            if (state)
            {
                isHardcoreOn = true;
                if (!hideMsg)
                { Game.DisplayHelp("Hardcore Mode ON"); }
                // Values update every tick so the function is not placed here
            }
            else
            {
                isHardcoreOn = false;
                if (!hideMsg)
                { Game.DisplayHelp("Hardcore Mode OFF"); }
                UpdateValues();
            }
        }

        private static void UpdateValues()
        {
            Player player = Game.LocalPlayer;

            if (isHardcoreOn)
            {
                Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 3.0f);
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 0.6f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.2f);
            }
            else
            {
                Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 1.0f);
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 1.0f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.0f);
            }
        }

        public static void End()
        {
            SetHardcore(false, true);
        }
    }
}
