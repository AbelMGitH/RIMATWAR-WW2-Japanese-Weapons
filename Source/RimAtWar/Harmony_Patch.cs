using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace HarkonRimAtWar
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            new Harmony("Harkon.RIMATWAR.WW2JapaneseWeapons").PatchAll();
        }
    }
    
    public static class Patch_FloatMenuMakerMap
    {        
        [HarmonyPatch(typeof(FloatMenuMakerMap), "AddHumanlikeOrders")]
        public static class AddHumanlikeOrders_Fix
        {
            public static void Postfix(Vector3 clickPos, Pawn pawn, ref List<FloatMenuOption> opts)
            {
                IntVec3 c = IntVec3.FromVector3(clickPos);
                if (pawn.equipment != null)
                {
                    List<Thing> thingList = c.GetThingList(pawn.Map);
                    for (int i = 0; i < thingList.Count; i++)
                    {
                        var options = thingList[i].def.GetModExtension<FlamethrowerPack>();
                        if (options != null)
                        {
                            var equipment = (ThingWithComps)thingList[i];
                            TaggedString toCheck = "Equip".Translate(equipment.LabelShort);
                            FloatMenuOption floatMenuOption = opts.FirstOrDefault((FloatMenuOption x) => x.Label.Contains(toCheck));
                            if (floatMenuOption != null && !CanEquip(pawn, options))
                            {
                                opts.Remove(floatMenuOption);
                                opts.Add(new FloatMenuOption("CannotEquip".Translate(equipment.LabelShort) + "(Requires a flamethrower fuel pack to be eqquiped)", null));
                            }
                            break;
                        }
                    }
                }
            }

            public static bool CanEquip(Pawn pawn, FlamethrowerPack options)
            {
                if (pawn.apparel.WornApparel != null)
                {
                    foreach (var wornApparel in pawn.apparel.WornApparel)
                    {                       
                            if (options.thingDef?.Contains(wornApparel.def.defName) ?? false)
                            {
                                return true;
                            }   
                    }
                }
                return false;
            }
        }
    }
}