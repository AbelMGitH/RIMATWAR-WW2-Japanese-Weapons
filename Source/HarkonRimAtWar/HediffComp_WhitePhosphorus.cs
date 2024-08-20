using RimWorld;
using UnityEngine;
using Verse;

namespace HarkonRimAtWar
{
    public class HediffComp_WhitePhosphorus : HediffComp
    {
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            bool flag = Gen.IsHashIntervalTick(base.Pawn, 60);
            if (flag)
            {
                bool flag2 = GenCollection.Any<Thing>(GridsUtility.GetThingList(base.Pawn.Position, base.Pawn.Map), (Thing x) => x.def == ThingDefOf.Filth_FireFoam);
                if (flag2)
                {
                    severityAdjustment = -1000f;
                }
                else
                {
                    Fire fire = AttachmentUtility.GetAttachment(base.Pawn, ThingDefOf.Fire) as Fire;
                    bool flag3 = fire == null && base.Pawn.Spawned;
                    if (flag3)
                    {
                        FireUtility.TryAttachFire(base.Pawn, this.parent.Severity * 0.25f, null);
                    }
                    else
                    {
                        bool flag4 = fire != null;
                        if (flag4)
                        {
                            fire.fireSize = Mathf.Min(fire.fireSize + this.parent.Severity * 0.25f, 1.20f);
                        }
                    }
                }
            }
        }
    }
}