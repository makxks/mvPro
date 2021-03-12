using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWeaponComponents : MonoBehaviour
{
    public GameObject baseObject;
    public GameObject barrel;
    public GameObject stock;
    public GameObject sights;
    public GameObject mag;

    public GameObject baseDisplay;
    public GameObject barrelDisplay;
    public GameObject stockDisplay;
    public GameObject sightsDisplay;
    public GameObject magDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBase(GameObject _base)
    {
        baseObject = _base;
        baseDisplay.GetComponent<SpriteRenderer>().sprite = _base.GetComponent<SpriteRenderer>().sprite;
    }

    public void setBarrel(GameObject _barrel)
    {
        barrel = _barrel;
        barrelDisplay.GetComponent<SpriteRenderer>().sprite = _barrel.GetComponent<SpriteRenderer>().sprite;
    }

    public void setStock(GameObject _stock)
    {
        stock = _stock;
        stockDisplay.GetComponent<SpriteRenderer>().sprite = _stock.GetComponent<SpriteRenderer>().sprite;

    }

    public void setsights(GameObject _sights)
    {
        sights = _sights;
        sightsDisplay.GetComponent<SpriteRenderer>().sprite = _sights.GetComponent<SpriteRenderer>().sprite;
    }

    public void setMag(GameObject _mag)
    {
        mag = _mag;
        magDisplay.GetComponent<SpriteRenderer>().sprite = _mag.GetComponent<SpriteRenderer>().sprite;
    }
}
