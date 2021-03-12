using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLineEffect : MonoBehaviour
{
    public GameObject parent;
    LineRenderer line;
    Color color1;
    Color color2;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        color1 = line.startColor;
        color2 = line.endColor;
        StartCoroutine(fade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator fade()
    {
        while (color1.a > 0)
        {
            color1 = new Color(color1.r, color1.g, color1.b, color1.a - 0.2f);
            color2 = new Color(color2.r, color2.g, color2.b, color2.a - 0.2f);
            line.startColor = color1;
            line.endColor = color2;
            yield return new WaitForEndOfFrame();
        }
        Destroy(parent);
    }
}
