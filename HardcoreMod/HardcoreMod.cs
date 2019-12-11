﻿namespace HardcoreMod
{
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreMod
    {
        private static bool isHardcoreOn = false;

        public static void Start()
        {
            SetHardcore(true, true);
            GameFiber.Sleep(3500);
            Game.DisplayHelp("Hardcore Mode is on.\nPress F2 to toggle it.\nGood luck!");
        }

        public static void Process()
        {
            if (Game.WasKeyJustPressed(Keys.F2))
            {
                SetHardcore(!isHardcoreOn);
            }
        }

        private static void SetHardcore(bool state, bool hideMsg = false)
        {
            Player player = Game.LocalPlayer;

            if (state)
            {
                Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 1.0f);
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 1.0f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.0f);
                isHardcoreOn = false;
                if (!hideMsg) { Game.DisplayHelp("Hardcore Mode OFF"); }
            }
            else
            {
                Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 3.0f);
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 0.5f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.2f);
                isHardcoreOn = true;
                if (!hideMsg) { Game.DisplayHelp("Hardcore Mode ON"); }
            }
        }

        public static void End()
        {
            SetHardcore(false, true);
        }
    }
}
