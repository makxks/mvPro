using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class LightningGunEffect : MonoBehaviour
{
    public List<LineRenderer> lines;
    private float timer;
    [SerializeField]
    private float changeTime = 1;
    [SerializeField]
    private float lightningWidth = 0.05f;
    [SerializeField]
    private float lengthRandomizer = 0.025f;
    private GunController gControl;

    private void Start()
    {
        gControl = GameObject.FindGameObjectWithTag("Player").GetComponent<GunController>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > changeTime)
        {
            resetLines();
        }
    }

    private void OnEnable()
    {
        resetLines();
    }

    private void resetLines()
    {
        timer = 0;
        setLines();
    }

    private void setLines()
    {
        Vector3[] positions = gControl.rayPositions;
        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].positionCount = positions.Length;
            for(int j=1; j<positions.Length; j++)
            {
                float randomY = Random.Range(-1 * lightningWidth, lightningWidth);
                float randomX = (j == positions.Length) ? 0f : Random.Range(-1 * lengthRandomizer, lengthRandomizer);
                positions[j] = new Vector3(positions[j].x + randomX, positions[j].y + randomY);
            }
            lines[i].SetPositions(positions);
        }
    }
}
