namespace HardcoreMod
{
    using System.Windows.Forms;
    using Rage;
    internal static class HardcoreMod
    {
        private static bool isDamageModifierOn = false;

        public static void Start()
        {
            // Announce us to the world :)
            GameFiber.Sleep(3500);
            Game.DisplayHelp("Welcome to hardcore mod! Version 0.1");
            GameFiber.Sleep(6000);
            Game.DisplayHelp("Hit F2 to toggle Hardcore Mode!");
            GameFiber.Sleep(6000);
            Game.DisplayHelp("Good luck! - Developed by Unwound & Vesdii");
        }

        public static void Process()
        {
            // Our main logic, processing the different stages in our mod.
            //GameFiber.Sleep(5000);

            ProcessInputs();

        }
        private static void ProcessInputs()
        {

            if (Game.WasKeyJustPressed(Keys.F2))
            {
                Player player = Game.LocalPlayer;

                if (isDamageModifierOn)
                {
                    isDamageModifierOn = false;  //Toggle Off
                    Game.DisplayHelp("Hardcore Disabled!");
                    Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 1.0f);
                    Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 1.0f);
                }
                else
                {
                    isDamageModifierOn = true; //Toggle on
                    Game.DisplayHelp("Hardcore Enabled!");
                    Game.CallNative("SET_AI_WEAPON_DAMAGE_MODIFIER", 3.0f); //Set weapon modifier
                    Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 0.75f); //Set Player Health Recharge Modifier
                }

            }
        }
        public static void End()
        {
            // You would free any resources here when the plugin is being unloaded.
        }
    }
}