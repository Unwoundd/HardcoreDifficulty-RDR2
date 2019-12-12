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
            SetHardcore(true, true);
            GameFiber.Sleep(4000);
            Game.DisplayHelp("Welcome to Hardcore mod!\nVersion 0.1");
            GameFiber.Sleep(4000);
            Game.DisplayHelp("Press F2 to toggle Hardcore Mode.\nGood luck!");
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
            Player player = Game.LocalPlayer;

            if (state)
            {
                isHardcoreOn = false;
                if (!hideMsg)
                { Game.DisplayHelp("Hardcore Mode OFF"); }
                UpdateValues();
            }
            else
            {
                isHardcoreOn = true;
                if (!hideMsg)
                { Game.DisplayHelp("Hardcore Mode ON"); }
                // Values update every tick so the function is not placed here
            }
        }
        private static void UpdateValues()
        {
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
