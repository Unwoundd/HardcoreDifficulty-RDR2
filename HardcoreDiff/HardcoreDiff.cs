namespace HardcoreDiff {
    using Rage;
    using System.Windows.Forms;

    internal static class HardcoreDiff {
        private const string modVersion = "1.2.1";
        private const float multDamageTaken = 4.0f;
        private const float multHealthRecharge = 0.05f;
        private const float multWeaponDamage = 1.2f;
        private static float lastHealth = 0.0f;
        private static int lastHealthCore = 0;
        //private static int lastStaminaCore = 0;
        private static int lastDeadEyeCore = 0;
        private static bool isHardcoreOn = false;
        private static bool isRespawning = false;

        public static void Start() {
            SetHardcore(true, false);
            GameFiber.Sleep(3000);
            Game.DisplayHelp("Hardcore Mode is ON.\nPress F2 to toggle it.\nv" + modVersion);
        }

        public static void Process() {
            Ped plyr = Game.LocalPlayer.Character;
            
            // Toggle hardcore mode
            if (Game.WasKeyJustPressed(Keys.F2)) {
                SetHardcore(!isHardcoreOn);
            }

            if (isHardcoreOn) {
                UpdateValues();

                // Increase damage taken
                if (plyr.Health < lastHealth) {
                    plyr.Health = lastHealth - ((lastHealth - plyr.Health) * multDamageTaken);
                }

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

                // Only recover stamina core on sleep
                if (!Game.LocalPlayer.HasControl
                        && lastHealthCore < 100
                        && plyr.HealthCore == 100) {
                    plyr.HealthCore = lastHealthCore;
                    plyr.DeadEyeCore = lastDeadEyeCore;
                }

                lastHealth = plyr.Health;
                lastHealthCore = plyr.HealthCore;
                //lastStaminaCore = plyr.StaminaCore;
                lastDeadEyeCore = plyr.DeadEyeCore;
            }

            /* Testing zone
            if (Game.WasKeyJustPressed(Keys.F7)) {
                plyr.Kill();
            }
            if (Game.WasKeyJustPressed(Keys.F8)) {
                plyr.HealthCore = 0;
                plyr.StaminaCore = 0;
                plyr.DeadEyeCore = 0;
            }
            if (Game.WasKeyJustPressed(Keys.F9)) {
                plyr.HealthCore = 100;
                plyr.StaminaCore = 100;
                plyr.DeadEyeCore = 100;
            }
            if (Game.WasKeyJustPressed(Keys.D0)) {
                Game.DisplayHelp("Timescale: " + Game.TimeScale);
            }
            if (Game.WasKeyJustPressed(Keys.OemMinus)) {
                Game.TimeScale -= 0.1f;
                Game.DisplayHelp("Timescale: " + Game.TimeScale);
            }
            if (Game.WasKeyJustPressed(Keys.Oemplus)) {
                Game.TimeScale += 0.1f;
                Game.DisplayHelp("Timescale: " + Game.TimeScale);
            }
            */
        }

        private static void SetHardcore(bool state, bool showMsg = true) {
            if (state) {
                isHardcoreOn = true;
                if (showMsg) {
                    Game.DisplayHelp("Hardcore Mode ON");
                }
            } else {
                isHardcoreOn = false;
                UpdateValues();
                if (showMsg) {
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
            SetHardcore(false, false);
        }
    }
}
