using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeComponents : MonoBehaviour
{
    public GameObject shortRangeBase;
    public List<GameObject> shortRangeBarrels;
    public List<GameObject> shortRangeStocks;
    public List<GameObject> shortRangeSights;
    public List<GameObject> shortRangeMags;
    // Start is called before the first frame update
    public SetWeaponComponents componentSetter;

    public void selectBarrel(int i) 
    {
        GameObject barrelToSet = shortRangeBarrels[i];
        componentSetter.setBarrel(barrelToSet);
        return;
    }

    public void selectStock(int i)
    {
        GameObject stockToSet = shortRangeStocks[i];
        componentSetter.setStock(stockToSet);
        return;
    }

    public void selectSights(int i)
    {
        GameObject sightsToSet = shortRangeSights[i];
        componentSetter.setsights(sightsToSet);
        return;
    }

    public void selectMag(int i)
    {
        GameObject magToSet = shortRangeMags[i];
        componentSetter.setMag(magToSet);
        return;
    }
}
