using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardSimV2
{
    class RepertoireSystem : ISystem
    {
        List<BardComponent> bardComponents;
        List<CooldownComponent> cooldownComponents;
        List<ModifierStateComponent> modifierStateComponents;
        List<RiverOfBloodComponent> riverOfBloodComponents;

        public RepertoireSystem(List<BardComponent> bardComponents, List<CooldownComponent> cooldownComponents, List<ModifierStateComponent> modifierStateComponents, List<RiverOfBloodComponent> riverOfBloodComponents)
        {
            this.bardComponents = bardComponents;
            this.cooldownComponents = cooldownComponents;
            this.modifierStateComponents = modifierStateComponents;
            this.riverOfBloodComponents = riverOfBloodComponents;
        }

        public void Update(decimal timer, Keyboard keyboard)
        {
            foreach (BardComponent brdComp in bardComponents)
            {
                if(brdComp.Song == SongName.MagesBallad)
                {
                    if (brdComp.Repertoire > 0)
                    {
                        RiverOfBloodComponent robComp = riverOfBloodComponents.Find(x => x.Parent == brdComp.Parent);

                        if(robComp != null)
                        {
                            foreach (Entity skill in robComp.SkillList)
                            {
                                CooldownComponent cdComp = cooldownComponents.Find(x => x.Parent == skill);

                                if(cdComp != null)
                                {
                                    if(cdComp.UsableAt >= timer + Constants.CooldownResetLock)
                                    {
                                        cdComp.UsableAt = timer + Constants.CooldownResetLock;
                                    }
                                }

                            }
                        }

                        brdComp.Repertoire = 0;

                    }
                }
                else if(brdComp.Song == SongName.ArmysPaeon)
                {
                    if(brdComp.Repertoire > 0)
                    {
                        Buff apEffect = null;
                        ModifierStateComponent brdModComp = modifierStateComponents.Find(x => x.Parent == brdComp.Parent);

                        if (brdModComp != null)
                        {
                            apEffect = brdModComp.BuffList.Find(x => x.Name == StatusName.ArmysPaeon);
                        }

                        if (apEffect != null)
                        {
                            brdModComp.BuffDictionary[apEffect.Type] -= apEffect.Modifier;
                            brdModComp.BuffList.Remove(apEffect);
                        }

                        apEffect = new Buff(brdModComp.Parent, brdModComp.Parent, StatusName.ArmysPaeon, AttributeType.SpeedType2, 30m, timer, brdComp.Repertoire * 4m, true);
                        brdModComp.BuffDictionary[apEffect.Type] += apEffect.Modifier;
                        brdModComp.BuffList.Add(apEffect);
                    }
                }
            }
        }
    }
}
