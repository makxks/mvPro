using System.Collections;
using System.Collections.Generic;

public class Gun
{
    public float damageBase { get; set; }
    public float damageMod { get; set; }
    private float damageFinal;

    public string projectileType { get; set; }

    public int magSizeBase { get; set; }
    public float magSizeMod { get; set; }
    private int magSizeFinal;

    public float reloadTimeBase { get; set; }
    public float reloadTimeMod { get; set; }
    private float reloadTimeFinal;

    public float stabilityBase { get; set; }
    public float stabilityMod { get; set; }
    private float stabilityFinal;

    public float accuracyBase { get; set; }
    public float accuracyMod { get; set; }
    private float accuracyFinal;
    
    public float projectileForceBase { get; set; }
    public float projectileForceMod { get; set; }
    private float projectileForceFinal;
    
    public float rangeBase { get; set; }
    public float rangeMod { get; set; }
    private float rangeFinal;
    
    public float delay { get; set; }
    public int kills { get; set; }
    public int precisionKills { get; set; }

    public Gun(
        float _damageBase,
        float _damageMod,
        int _magSizeBase,
        float _magSizeMod,
        float _reloadTimeBase,
        float _reloadTimeMod,
        float _stabilityBase,
        float _stabilityMod,
        float _accuracyBase,
        float _accuracyMod,
        float _projectileForceBase,
        float _projectileForceMod,
        float _rangeBase,
        float _rangeMod,
        float _delay,
        int _kills,
        int _precisionKills,
        string _projectileType
        )
        {
            damageBase = _damageBase;
            damageMod = _damageMod;
            damageFinal = damageBase * damageMod;

            magSizeBase = _magSizeBase;
            magSizeMod = _magSizeMod;
            magSizeFinal = (int)(magSizeBase * magSizeMod);

            reloadTimeBase = _reloadTimeBase;
            reloadTimeMod = _reloadTimeMod;
            reloadTimeFinal = reloadTimeBase * reloadTimeMod;

            stabilityBase = _stabilityBase;
            stabilityMod = _stabilityMod;
            stabilityFinal = stabilityBase * stabilityMod;

            accuracyBase = _accuracyBase;
            accuracyMod = _accuracyMod;
            accuracyFinal = accuracyBase * accuracyFinal;

            projectileForceBase = _projectileForceBase;
            projectileForceMod = _projectileForceMod;
            projectileForceFinal = projectileForceBase * projectileForceMod;

            rangeBase = _rangeBase;
            rangeMod = _rangeMod;
            rangeFinal = rangeBase * rangeFinal;

            delay = _delay;
            kills = _kills;
            precisionKills = _precisionKills;
            projectileType = _projectileType;
        }


    public void Shoot()
    {

    }

    public void modifyDamage(float modifier)
    {

    }

    public void modifyRange(float modifier)
    {

    }

    public void modifyMagSize(float modifier)
    {

    }

    public void modifyStability(float modifier)
    {

    }

    public void modifyAccuracy(float modifier)
    {

    }

    public void modifyReloadTime(float modifier)
    {

    }
}
