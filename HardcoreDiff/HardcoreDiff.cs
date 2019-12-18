namespace HardcoreDiff {
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreDiff {
        private static bool isHardcoreOn = false;
        private const float multDamageTaken = 3.0f;
        private const float multHealthRecharge = 0.05f;
        private const float multWeaponDamage = 1.15f;
        private static float lastHealth = 0.0f;

        public static void Start() {
            SetHardcore(true, true);
            GameFiber.Sleep(4000);
            Game.DisplayHelp("Hardcore Difficulty v0.2.E\nBy Unwound & Vesdii");
            GameFiber.Sleep(3000);
            Game.DisplayHelp("Hardcore Mode is on.\nPress F2 to toggle it.\nGood luck!");
        }

        public static void Process() {
            if (Game.WasKeyJustPressed(Keys.F2)) {
                SetHardcore(!isHardcoreOn);
            }
            
            if (isHardcoreOn) {
                UpdateValues();

                Ped playerPed = Game.LocalPlayer.Character;
                if (playerPed.Health < lastHealth) {
                    playerPed.Health = lastHealth - ((lastHealth - playerPed.Health) * multDamageTaken);
                    lastHealth = playerPed.Health;
                }
            }
        }

        private static void SetHardcore(bool state, bool hideMsg = false) {
            if (state) {
                isHardcoreOn = true;
                lastHealth = Game.LocalPlayer.Character.Health;
                if (!hideMsg) {
                    Game.DisplayHelp("Hardcore Mode ON");
                }
            } else {
                isHardcoreOn = false;
                UpdateValues();
                if (!hideMsg) {
                    Game.DisplayHelp("Hardcore Mode OFF");
                }
            }
        }

        private static void UpdateValues() {
            Player player = Game.LocalPlayer;

            if (isHardcoreOn) {
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, multHealthRecharge);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, multWeaponDamage);
            } else {
                Game.CallNative("SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER", player, 1.0f);
                Game.CallNative("SET_PLAYER_WEAPON_DAMAGE_MODIFIER", player, 1.0f);
            }
        }

        public static void End() {
            SetHardcore(false, true);
        }
    }
}
