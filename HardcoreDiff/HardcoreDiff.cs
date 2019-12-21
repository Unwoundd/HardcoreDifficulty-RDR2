namespace HardcoreDiff {
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreDiff {
        private const string modVersion = "1.2";
        private const float multDamageTaken = 4.0f;
        private const float multHealthRecharge = 0.05f;
        private const float multWeaponDamage = 1.2f;
        private static bool isHardcoreOn = false;
        private static bool isRespawning = false;
        private static float lastHealth = 0.0f;

        public static void Start() {
            SetHardcore(true, true);
            GameFiber.Sleep(3000);
            Game.DisplayHelp("Hardcore Mode is ON.\nPress F2 to toggle it.\nv" + modVersion);
        }

        public static void Process() {
            if (Game.WasKeyJustPressed(Keys.F2)) {
                SetHardcore(!isHardcoreOn);
            }

            if (isHardcoreOn) {
                UpdateValues();

                Ped plyr = Game.LocalPlayer.Character;

                // Increase damage taken
                if (plyr.Health < lastHealth) {
                    plyr.Health = lastHealth - ((lastHealth - plyr.Health) * multDamageTaken);
                }
                lastHealth = plyr.Health;

                // Drain cores on respawn in free roam
                if (!Game.IsMissionActive) {
                    if (isRespawning) {
                        GameFiber.Sleep(10000);
                        plyr.HealthCore = 0;
                        plyr.StaminaCore = 0;
                        plyr.DeadEyeCore = 0;
                        isRespawning = false;
                    } else if (plyr.IsDead) {
                        isRespawning = true;
                    }
                }
            }

            // Testing zone
            //if (Game.WasKeyJustPressed(Keys.F8)) {
            //    Ped plyr = Game.LocalPlayer.Character;

            //    plyr.Kill();
            //}
        }

        private static void SetHardcore(bool state, bool hideMsg = false) {
            if (state) {
                isHardcoreOn = true;
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
