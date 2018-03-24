using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
     internal static class CombatFormulas
    {
         internal static Random rng = new Random();

         internal static decimal AttackPowerDamageMod(int mainstat)
        {
            return Math.Floor((decimal)(125 * (mainstat - 292) / 292) + 100) / 100;
        }
         internal static decimal CriticalHitRate(int crit)
        {
            return Math.Floor(200 * ((decimal)crit - 364) / 2170 + 50) / 10;
        }
         internal static decimal CriticalHitDamageMod(int crit)
        {
            return Math.Floor((decimal)(200 * (crit - 364) / 2170 + 1400)) / 1000;
        }
         internal static decimal DirectHitRate(int dhit)
        {
            return Math.Floor(550 * ((decimal)dhit - 364) / 2170) / 10;
        }
         internal static decimal DirectHitDamageMod(int dhit)
        {
            return 1.25m;
        }
         internal static decimal DeterminationDamageMod(int det)
        {
            return Math.Floor((decimal)(130 * (det - 292) / 2170 + 1000)) / 1000;
        }
         internal static decimal SpeedDamageMod(int ss)
        {
            return Math.Floor((decimal)(130 * (ss - 364) / 2170 + 1000)) / 1000;
        }
         internal static decimal SpeedRecast(int ss, decimal recast, int arrow = 0, int haste = 0, bool hasFeyWind = false, bool hasLeyLines = false, bool hasPresenceOfMind = false, bool hasShifu = false, bool hasBloodWeapon = false, bool hasHuton = false, int greasedLightning = 0, int armysPaeon = 0, bool hasRiddleOfFire = false, bool hasAstralUmbral = false)
        {
            //decimal feyWind = Convert.ToInt32(hasFeyWind) * 3;
            //decimal type1 = Convert.ToInt32(hasLeyLines) * 15 + Convert.ToInt32(hasPresenceOfMind) * 20 + Convert.ToInt32(hasShifu) * 10 + Convert.ToInt32(hasBloodWeapon) * 10;
            //decimal type2 = Convert.ToInt32(hasHuton) * 15 + greasedLightning * 5 + armysPaeon * 4;
            //decimal GCDm = Math.Floor((1000 - Math.Floor(130 * (sks - 364) / 2170)) * 2.5 / 1000);
            //decimal A = Math.Floor(Math.Floor(Math.Floor((100 - arrow) * (100 - type1) / 100) * (100 - haste) / 100) - feyWind);
            //decimal B = (type2 - 100) / 100;
            decimal GCD = Math.Floor(Math.Floor(Math.Floor(Math.Ceiling(Math.Floor(Math.Floor(Math.Floor((decimal)((100 - arrow) * (100 - (Convert.ToInt32(hasLeyLines) * 15 + Convert.ToInt32(hasPresenceOfMind) * 20 + Convert.ToInt32(hasShifu) * 10 + Convert.ToInt32(hasBloodWeapon) * 10)) / 100)) * (100 - haste) / 100) - (Convert.ToInt32(hasFeyWind) * 3)) * ((Convert.ToInt32(hasHuton) * 15 + greasedLightning * 5 + armysPaeon * 4) - 100) / 100) * Math.Floor((1000 - Math.Floor((decimal)(130 * (ss - 364) / 2170))) * recast.SecondsToMilli() / 1000) / 100) * (Convert.ToInt32(hasRiddleOfFire) * 15 + 100) / 1000) * (100 - Convert.ToInt32(hasAstralUmbral) * 50) / 100);

            return GCD;

        }
         internal static decimal TenacityDamageMod(int ten)
        {
            return Math.Floor((decimal)(130 * (ten - 364) + 1000)) / 1000;
        }
         internal static decimal WeaponDamageMod(int wd)
        {
            return Math.Floor((decimal)(292 * 115 / 1000) + wd);
        }
         internal static decimal PotencyMod(int potency)
        {
            return (decimal)potency / 100;
        }
         internal static decimal AutoAttackMod(int wd, decimal delay)
        {
            return Math.Floor(Math.Floor((decimal)((292 * 115 / 1000) + wd)) * (delay / 3));
        }
         internal static decimal DirectDamage(decimal potMod, decimal wdMod, decimal apMod, decimal detMod, decimal tenMod, decimal traitMod, decimal critMod, decimal dhitMod, List<SpecialBuff> buffList)
        {
            decimal damage = Math.Floor(Math.Floor(Math.Floor(Math.Floor(potMod * wdMod * apMod * detMod * tenMod * traitMod) * critMod) * dhitMod) * ((decimal)rng.Next(95, 101) / 100));

            foreach (SpecialBuff b in buffList)
            {
                if(b.Attribute == SpecialBuffType.Damage)
                {
                    damage = Math.Floor(damage * b.Modifier);
                }
            }

            return damage;
        }
         internal static decimal DoTDamage(decimal potMod, decimal wdMod, decimal apMod, decimal detMod, decimal tenMod, decimal traitMod, decimal ssMod, decimal critMod, decimal dhitMod, List<SpecialBuff> buffList)
        {
            decimal damage = Math.Floor(Math.Floor(Math.Floor(Math.Floor(Math.Floor(potMod * wdMod * apMod * detMod * tenMod * traitMod) * ssMod) * ((decimal)rng.Next(95, 101) / 100)) * critMod) * dhitMod);

            foreach (SpecialBuff b in buffList)
            {
                if (b.Attribute == SpecialBuffType.Damage)
                {
                    damage = Math.Floor(damage * b.Modifier);
                }
            }

            return damage;
        }

         internal static decimal AutoAttackDamage(decimal potMod, decimal aaMod, decimal apMod, decimal detMod, decimal tenMod, decimal traitMod, decimal ssMod, decimal critMod, decimal dhitMod, List<SpecialBuff> buffList)
        {
            decimal damage = Math.Floor(Math.Floor(Math.Floor(Math.Floor(Math.Floor(potMod * aaMod * apMod * detMod * tenMod * traitMod) * ssMod) * critMod) * dhitMod) * ((decimal)rng.Next(95, 101) / 100));

            foreach (SpecialBuff b in buffList)
            {
                if (b.Attribute == SpecialBuffType.Damage)
                {
                    damage = Math.Floor(damage * b.Modifier);
                }
            }

            return damage;
        }
    }
}
