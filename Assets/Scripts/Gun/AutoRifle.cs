using System.Collections;
using System.Collections.Generic;

public class AutoRifle : Gun
{
    public AutoRifle(
        float damageBase,
        float damageMod,
        int magSizeBase,
        float magSizeMod,
        float reloadTimeBase,
        float reloadTimeMod,
        float stabilityBase,
        float stabilityMod,
        float accuracyBase,
        float accuracyMod,
        float projectileForceBase,
        float projectileForceMod,
        float rangeBase,
        float rangeMod,
        float delay,
        int kills,
        int precisionKills,
        string projectileType
        ) : base(
            damageBase,
            damageMod,
            magSizeBase,
            magSizeMod,
            reloadTimeBase,
            reloadTimeMod,
            stabilityBase,
            stabilityMod,
            accuracyBase,
            accuracyMod,
            projectileForceBase,
            projectileForceMod,
            rangeBase,
            rangeMod,
            delay,
            kills,
            precisionKills,
            projectileType
            )
        {
        }
}
