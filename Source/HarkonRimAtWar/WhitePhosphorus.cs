using System.Collections.Generic;
using RimWorld;
using Verse;

namespace HarkonRimAtWar
{
    public class WhitePhosphorus : Filth
    {
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            List<Thing> list = new List<Thing>(GridsUtility.GetThingList(base.Position, map));
            foreach (Thing thing in list)
            {
                Building_Door building_Door = thing as Building_Door;
                bool flag = building_Door != null && !building_Door.Open;
                if (flag)
                {
                    this.Destroy(0);
                    break;
                }
                bool flag2 = AttachmentUtility.HasAttachment(thing, ThingDefOf.Fire);
                if (flag2)
                {
                    Fire fire = (Fire)AttachmentUtility.GetAttachment(thing, ThingDefOf.Fire);
                    bool flag3 = fire != null;
                    if (flag3)
                    {
                        fire.fireSize = 1.00f;
                    }
                }
                else
                {
                    FireUtility.TryAttachFire(thing, 1.00f, null);
                }
            }
        }

        public override void Tick()
        {
            bool flag = GenCollection.Any<Thing>(GridsUtility.GetThingList(base.Position, base.Map), (Thing x) => x.def == ThingDefOf.Filth_FireFoam);
            if (flag)
            {
                bool flag2 = !base.Destroyed;
                if (flag2)
                {
                    this.Destroy(0);
                }
            }
            else
            {
                FireUtility.TryStartFireIn(base.Position, base.Map, 1.00f, this, null);
            }
        }
    }
}