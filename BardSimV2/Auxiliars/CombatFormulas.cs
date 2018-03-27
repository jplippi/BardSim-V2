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

         internal static decimal AttackPowerDamageMod(decimal mainstat)
        {
            return Math.Floor(125 * (mainstat - 292) / 292 + 100) / 100;
        }
         internal static decimal CriticalHitRate(decimal crit)
        {
            return Math.Floor(200 * (crit - 364) / 2170 + 50) / 10;
        }
         internal static decimal CriticalHitDamageMod(decimal crit)
        {
            return Math.Floor(200 * (crit - 364) / 2170 + 1400) / 1000;
        }
         internal static decimal DirectHitRate(decimal dhit)
        {
            return Math.Floor(550 * (dhit - 364) / 2170) / 10;
        }
         internal static decimal DirectHitDamageMod(decimal dhit)
        {
            return 1.25m;
        }
         internal static decimal DeterminationDamageMod(decimal det)
        {
            return Math.Floor(130 * (det - 292) / 2170 + 1000) / 1000;
        }
         internal static decimal SpeedDamageMod(decimal ss)
        {
            return Math.Floor(130 * (ss - 364) / 2170 + 1000) / 1000;
        }
         internal static decimal SpeedRecast(decimal ss, decimal recast, decimal arrow = 0, decimal haste = 0, decimal feyWind = 0, decimal speedType1 = 0, decimal speedType2 = 0, decimal riddleOfFire = 100, decimal astralUmbral = 100)
        {
            return Math.Floor(Math.Floor(Math.Floor(Math.Ceiling(Math.Floor(Math.Floor(Math.Floor((100 - arrow) * (100 - (speedType1)) / 100) * (100 - haste) / 100) - feyWind) * ((speedType2) - 100) / -100) * Math.Floor((1000 - Math.Floor(130 * (ss - 364) / 2170)) * (decimal)recast.SecondsToMilli() / 1000) / 100) * riddleOfFire / 1000) * astralUmbral / 100) / 100;
        }
         internal static decimal TenacityDamageMod(decimal ten)
        {
            return Math.Floor(130 * (ten - 364) + 1000) / 1000;
        }
         internal static decimal WeaponDamageMod(decimal wd)
        {
            return Math.Floor(292 * 115 / 1000 + wd);
        }
         internal static decimal PotencyMod(decimal potency)    
        {
            return potency / 100;
        }
         internal static decimal AutoAttackMod(decimal wd, decimal delay)
        {
            return Math.Floor(Math.Floor((292 * 115 / 1000) + wd) * (delay / 3));
        }
         internal static decimal DirectDamage(decimal potMod, decimal wdMod, decimal apMod, decimal detMod, decimal tenMod, decimal traitMod, decimal critMod, decimal dhitMod, List<Buff> buffList)
        {
            decimal damage = Math.Floor(Math.Floor(Math.Floor(potMod * wdMod * apMod * detMod * tenMod * traitMod) * critMod) * dhitMod);

            damage = rng.Next((int)Math.Floor(damage * 0.95m), (int)Math.Floor(damage * 1.05m) + 1);

            foreach (Buff b in buffList)
            {
                if(b.Type == AttributeType.Damage && b.IsActive)
                {
                    damage = Math.Floor(damage * b.Modifier);
                }
            }

            return damage;
        }
         internal static decimal DoTDamage(decimal potMod, decimal wdMod, decimal apMod, decimal detMod, decimal tenMod, decimal traitMod, decimal ssMod, decimal critMod, decimal dhitMod, List<Buff> buffList)
        {
            decimal damage = Math.Floor(Math.Floor(potMod * wdMod * apMod * detMod * tenMod * traitMod) * ssMod);

            damage = rng.Next((int)Math.Floor(damage * 0.95m), (int)Math.Floor(damage * 1.05m) + 1);

            damage = Math.Floor(Math.Floor(damage * critMod) * dhitMod);

            foreach (Buff b in buffList)
            {
                if (b.Type == AttributeType.Damage && b.IsActive)
                {
                    damage = Math.Floor(damage * b.Modifier);
                }
            }

            return damage;
        }

         internal static decimal AutoAttackDamage(decimal potMod, decimal aaMod, decimal apMod, decimal detMod, decimal tenMod, decimal traitMod, decimal ssMod, decimal critMod, decimal dhitMod, List<Buff> buffList)
        {
            decimal damage = Math.Floor(Math.Floor(Math.Floor(Math.Floor(potMod * aaMod * apMod * detMod * tenMod * traitMod) * ssMod) * critMod) * dhitMod);

            damage = rng.Next((int)Math.Floor(damage * 0.95m), (int)Math.Floor(damage * 1.05m) + 1);

            foreach (Buff b in buffList)
            {
                if (b.Type == AttributeType.Damage && b.IsActive)
                {
                    damage = Math.Floor(damage * b.Modifier);
                }
            }

            return damage;
        }
    }
}
