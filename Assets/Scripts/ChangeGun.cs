using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    public List<GameObject> guns;

    private GunController gControl;

    private void Start()
    {
        gControl = GetComponent<GunController>();   
    }

    public void selectGun(string gunName)
    {
        for(int i=0; i<guns.Count; i++)
        {
            if(guns[i].GetComponent<GunInstance>().GunName == gunName)
            {
                guns[i].GetComponent<SpriteRenderer>().enabled = true;
                gControl.setActiveGun(guns[i], guns[i].GetComponent<GunInstance>().getType());
            }
            else
            {
                guns[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
