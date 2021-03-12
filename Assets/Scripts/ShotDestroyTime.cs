using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDestroyTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;
    private bool timerStart = false;
    private bool destroyed = false;
    private float timer = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        timerStart = true;
    }

    private void Update()
    {
        if (timerStart)
        {
            timer += Time.deltaTime;
            if(timer > destroyTime && !destroyed)
            {
                destroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
