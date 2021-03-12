using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{
    public GameObject shotContainer;
    public GameObject shotContainerForTrail;
    public GameObject shotContainerForRay;
    public GameObject gunEnd;
    [SerializeField]
    private float shotForce = 100f;
    private float shotForceForRay = 1000f;
    [SerializeField]
    private float shotDelay = 0.1f;
    private float shotTimer = 0;
    private bool canShoot;
    private string activeType;
    private GameObject activeGun;
    public bool useForce;
    public bool useRay;
    public bool useTrail;
    private bool shootingRay = false;
    public bool particles = false;
    [SerializeField]
    private LayerMask layer;
    ParticleSystem[] particleArray;

    public Vector3[] rayPositions;
    [SerializeField]
    private float lightningLength = 1;

    [SerializeField]
    private float aimSpeed = 0.2f;

    [SerializeField]
    private float accuracy = 1;
    // Start is called before the first frame update
    void Start()
    {
        activeType = "trace";
    }

    // Update is called once per frame
    void Update()
    {
        if (activeGun)
        {
            aim();
            if (useTrail)
            {
                shoot2();
            }
            else if (useRay)
            {
                shoot3();
                stopShot();
            }
            else if (useForce)
            {
                shoot();
            }

            if (!canShoot)
            {
                shotTimer += Time.deltaTime;
                if (shotTimer > shotDelay)
                {
                    canShoot = true;
                    shotTimer = 0;
                }
            }
        }
    }

    private void shoot()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;

            GameObject cloneShot = Instantiate(shotContainer, gunEnd.transform.position, gunEnd.transform.rotation);
            // add inaccuracy as a component of relative force
            // needs to be a maximum of about 10% of shot force
            var inaccuracy = (shotForce / 10) - (accuracy * (shotForce / 1000));
            // shotForce / 10 == 10% of shotForce
            // accuracy at max is 100, so shotForce/1000 * 100 == shotForce/10; Therefore at 100 accuracy inaccuracy is 0, at 0 accuracy inaccuracy is 10% shotForce;
            var randomForce = Random.Range(-inaccuracy, inaccuracy);
            if (cloneShot.GetComponentInChildren<Rigidbody2D>())
            {
                Rigidbody2D shot = cloneShot.GetComponentInChildren<Rigidbody2D>();
                if (activeGun.transform.localScale.x < 0)
                {
                    cloneShot.transform.localScale = new Vector3(-1, 1, 1);
                    shot.AddRelativeForce(new Vector2(-1 * shotForce, randomForce));
                }
                else
                {
                    shot.AddRelativeForce(new Vector2(shotForce, 0));
                }
            }
            else if (cloneShot.GetComponentInChildren<Rigidbody>())
            {
                Rigidbody shot = cloneShot.GetComponentInChildren<Rigidbody>();
                if (activeGun.transform.localScale.x < 0)
                {
                    cloneShot.transform.localScale = new Vector3(-1, 1, 1);
                    shot.AddRelativeForce(new Vector2(-1 * shotForce, randomForce));
                }
                else
                {
                    shot.AddRelativeForce(new Vector2(shotForce, 0));
                }
            }
            recoil(50);
        }
    }

    private void shoot2()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            if (activeType == "Spread Shot")
            {
                for (int i = 0; i < 5; i++)
                {
                    GameObject cloneShot = Instantiate(shotContainerForTrail, gunEnd.transform.position, gunEnd.transform.rotation);
                    var inaccuracy = (shotForce / 10) - (accuracy * (shotForce / 1000));
                    var randomForce = Random.Range(-inaccuracy, inaccuracy);
                    float randomZ = Random.Range(-200, 200 + randomForce);
                    if (cloneShot.GetComponentInChildren<Rigidbody2D>())
                    {
                        Rigidbody2D shot = cloneShot.GetComponentInChildren<Rigidbody2D>();
                        shot.transform.Rotate(new Vector3(0, 0, randomZ / 120));
                        if (activeGun.transform.localScale.x < 0)
                        {
                            cloneShot.transform.localScale = new Vector3(-1, 1, 1);
                            shot.AddRelativeForce(new Vector2(-1 * shotForceForRay, randomZ));
                        }
                        else
                        {
                            shot.AddRelativeForce(new Vector2(shotForceForRay, randomZ));
                        }
                    }
                    else if (cloneShot.GetComponentInChildren<Rigidbody>())
                    {
                        Rigidbody shot = cloneShot.GetComponentInChildren<Rigidbody>();
                        shot.transform.Rotate(new Vector3(0, 0, randomZ / 120));
                        if (activeGun.transform.localScale.x < 0)
                        {
                            cloneShot.transform.localScale = new Vector3(-1, 1, 1);
                            shot.AddRelativeForce(new Vector2(-1 * shotForceForRay, randomZ));
                        }
                        else
                        {
                            shot.AddRelativeForce(new Vector2(shotForceForRay, randomZ));
                        }
                    }
                }
                recoil(10);
            }
            else
            {
                GameObject cloneShot = Instantiate(shotContainerForTrail, gunEnd.transform.position, gunEnd.transform.rotation);
                // add inaccuracy as a component of relative force
                // needs to be a maximum of about 10% of shot force
                var inaccuracy = (shotForce / 10) - (accuracy * (shotForce / 1000));
                // shotForce / 10 == 10% of shotForce
                // accuracy at max is 100, so shotForce/1000 * 100 == shotForce/10; Therefore at 100 accuracy inaccuracy is 0, at 0 accuracy inaccuracy is 10% shotForce;
                var randomForce = Random.Range(-inaccuracy, inaccuracy);
                if (cloneShot.GetComponentInChildren<Rigidbody2D>())
                {
                    Rigidbody2D shot = cloneShot.GetComponentInChildren<Rigidbody2D>();
                    if (activeGun.transform.localScale.x < 0)
                    {
                        cloneShot.transform.localScale = new Vector3(-1, 1, 1);
                        shot.AddRelativeForce(new Vector2(-1 * shotForceForRay, randomForce));
                    }
                    else
                    {
                        shot.AddRelativeForce(new Vector2(shotForceForRay, randomForce));
                    }
                }
                else if (cloneShot.GetComponentInChildren<Rigidbody>())
                {
                    Rigidbody shot = cloneShot.GetComponentInChildren<Rigidbody>();
                    if (activeGun.transform.localScale.x < 0)
                    {
                        cloneShot.transform.localScale = new Vector3(-1, 1, 1);
                        shot.AddRelativeForce(new Vector2(-1 * shotForceForRay, randomForce));
                    }
                    else
                    {
                        shot.AddRelativeForce(new Vector2(shotForceForRay, randomForce));
                    }
                }
                recoil(50);
            }
        }
    }

    private void shoot3()
    {
        if(Input.GetMouseButton(0) && canShoot)
        {
            shotContainerForRay.SetActive(true);
            LineRenderer line = shotContainerForRay.GetComponentInChildren<LineRenderer>();
            Vector3 start = gunEnd.transform.position;
            Ray ray = new Ray();
            ray.origin = gunEnd.transform.position;
            //accuracy for ray based weapon must be defined differently
            //end up with a transform.up component that is a fraction of the transform.right component
            //the transform.up component is the inaccuracy
            var inaccuracy = 0.05f - ((accuracy/100) * 0.05f);
            var randomForce = Random.Range(-inaccuracy, inaccuracy);
            Vector3 finalTrajectory = gunEnd.transform.right + gunEnd.transform.up * randomForce;
            if (activeGun.transform.localScale.x < 0)
            {
                ray.direction = -finalTrajectory;
            }
            else
            {
                ray.direction = finalTrajectory;
            }
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(ray, out hitInfo, 100, layer);
            Vector2 end;
            if (hit)
            {
                end = hitInfo.point;
            }
            else
            {
                end = ray.GetPoint(100);
            }
            if (activeType == "Trace Rifle")
            {
                rayPositions = new Vector3[2];
                rayPositions[0] = start;
                rayPositions[1] = end;
                line.SetPositions(rayPositions);
            }
            else if(activeType == "Lightning Rifle")
            {
                float length = Vector2.Distance(start, end);
                rayPositions = new Vector3[(int)((length / lightningLength) > 2 ? length / lightningLength : 2)];
                float xIncrement = (end.x - start.x) / rayPositions.Length;
                float yIncrement = (end.y - start.y) / rayPositions.Length;
                rayPositions[0] = start;
                for(int i = 1; i < rayPositions.Length - 1; i++)
                {
                    rayPositions[i] = new Vector3(start.x + (i * xIncrement), start.y + (i * yIncrement));
                }
                rayPositions[(int)((length / lightningLength) > 2 ? (length / lightningLength) - 1 : 1)] = end;
            }
            if (particles)
            {
                shotContainerForRay.transform.position = activeGun.transform.position;
                shotContainerForRay.transform.rotation = activeGun.transform.rotation;
                shotContainerForRay.transform.localScale = activeGun.transform.localScale;
                //particles1 = shotContainerForRay.GetComponentsInChildren<ParticleSystem>();
                //ParticleSystem.ShapeModule shape = particles1.shape;
                //shape.position = ray.GetPoint(50);
                //shape.radius = 50;
                for(int i=0; i<particleArray.Length; i++)
                {
                    ParticleSystem.ShapeModule shape = particleArray[i].shape;
                    ParticleSystem.EmissionModule emission = particleArray[i].emission;
                    float length = Vector2.Distance(start, end);
                    float xPos = (length / 2f) + 0.2f;
                    shape.position = new Vector2(xPos, 0);
                    shape.radius = length / 2;
                    emission.rateOverTime = 50 * length;
                }
            }
        }
    }


    private void stopShot()
    {
        if (Input.GetMouseButtonUp(0))
        {
            shotContainerForRay.SetActive(false);
        }
    }

    private void aim()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(activeGun.transform.position);
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        // add aim limits here
        if (dir.x < 0)
        {
            activeGun.transform.localScale = new Vector2(-1, 1);
            angle = angle - 180;
            Quaternion newAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            activeGun.transform.rotation = Quaternion.Lerp(activeGun.transform.rotation, newAngle, aimSpeed);
        }
        else
        {
            //angle = Mathf.Clamp(angle, -60, 60); only works on this side will have to see if there is a way to get both sides to work 360 degree aiming is ok
            activeGun.transform.localScale = new Vector2(1, 1); // will actually rotate character
            Quaternion newAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            activeGun.transform.rotation = Quaternion.Lerp(activeGun.transform.rotation, newAngle, aimSpeed);
        }
    }

    public string getActiveType()
    {
        return activeType;
    }

    public void setActiveGun(GameObject gun, string type)
    {
        activeType = type;
        activeGun = gun;
        GunInstance activeGunGScript = activeGun.GetComponent<GunInstance>();
        gunEnd = activeGunGScript.gunEnd;
        shotContainer = activeGunGScript.shotContainer;
        shotContainerForTrail = activeGunGScript.shotContainerForTrail;
        if (shotContainerForRay)
        {
            shotContainerForRay.SetActive(false);
        }
        shotContainerForRay = activeGunGScript.shotContainerForRay;
        shotDelay = activeGunGScript.getDelay();
        shotForce = activeGunGScript.getForce();
        shotForceForRay = activeGunGScript.getRayForce();
        if (shotContainerForRay)
        {
            particleArray = shotContainerForRay.GetComponentsInChildren<ParticleSystem>();
        }
        switch (type)
        {
            case "Trace Rifle":
                useForce = false;
                useTrail = false;
                useRay = true;
                particles = true;
                break;
            case "Lightning Rifle":
                useForce = false;
                useTrail = false;
                useRay = true;
                particles = false;
                break;
            case "Auto Rifle":
                useForce = false;
                useTrail = true;
                useRay = false;
                particles = false;
                break;
            case "Grenade Launcher":
                useForce = true;
                useTrail = false;
                useRay = false;
                particles = false;
                break;
            case "Spread Shot":
                useForce = false;
                useTrail = true;
                useRay = false;
                particles = false;
                break;
        }
    }

    public void recoil(float stability)
    {
        float recoil = 200f;
        float tempRecoil = recoil / stability;
        activeGun.transform.eulerAngles = new Vector3(activeGun.transform.eulerAngles.x, activeGun.transform.eulerAngles.y, activeGun.transform.eulerAngles.z + tempRecoil);
    }
}
