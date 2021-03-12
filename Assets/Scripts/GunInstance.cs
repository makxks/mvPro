using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunInstance : MonoBehaviour
{
    [SerializeField]
    public string GunName;
    [SerializeField]
    private string type;
    public GameObject gunEnd;
    public GameObject shotContainer;
    public GameObject shotContainerForTrail;
    public GameObject shotContainerForRay;

    [SerializeField]
    private float shotDelay;
    [SerializeField]
    private bool usesForce;
    [SerializeField]
    private float shotForce;
    [SerializeField]
    private float shotForceForRay;
    // Start is called before the first frame update

    private void Start()
    {
    }

    public string getType()
    {
        return type;
    }

    public bool useForce()
    {
        return usesForce;
    }


    public float getForce()
    {
        return shotForce;
    }

    public float getDelay()
    {
        return shotDelay;
    }

    public float getRayForce()
    {
        return shotForceForRay;
    }
}
