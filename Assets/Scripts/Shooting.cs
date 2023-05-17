using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Globalization;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    public struct weaponData
    {
        public string weaponName;
        public string weaponType;
        public int magCapacity;
        public int maxAmmo;
        public float FiringTime;
        public float damage;
        public float reloadTime;
        public int currentAmmo;
        public weaponData(string name,string type, int magcap, int maxammo, float time, float dmg,float reload,int ammo) 
        {
            weaponName = name;
            weaponType = type;
            magCapacity = magcap;
            maxAmmo = maxammo;
            FiringTime = time;
            damage = dmg;
            reloadTime = reload;
            currentAmmo = ammo;
        }
    }
    Camera mainCam;
    [SerializeField]Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire=true;
    private float timer;
    public float timeBetweenFiring= 0.5f;
    public int weaponNumber=2;
    public List<weaponData> weaponDatas = new List<weaponData>();
    public weaponData equippedWeapon;
    public int currentMagAmmo;
    public bool reloading;
    public bool empty=false;
    Quaternion offset = Quaternion.Euler(0, 0, 30);
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        string[] lines = File.ReadAllLines("Assets/Data/weaponinfo.txt");
        foreach (var line in lines)
        {
            string[] oneLine = line.Split();
            weaponDatas.Add(new weaponData(oneLine[0], oneLine[1], int.Parse(oneLine[2]), int.Parse(oneLine[3]), float.Parse(oneLine[4], new CultureInfo("en-UK")),float.Parse(oneLine[5], new CultureInfo("en-UK")),float.Parse(oneLine[6], new CultureInfo("en-UK")),int.Parse(oneLine[7])));
        }
        Debug.Log(weaponDatas[1].FiringTime);
        equippedWeapon = weaponDatas[weaponNumber];
        equippedWeapon.currentAmmo = equippedWeapon.magCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(equippedWeapon.weaponType);
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer>equippedWeapon.FiringTime)
            {
                canFire = true;
                timer = 0;
            }
        }
        //Debug.Log(equippedWeapon.damage);
        if (Input.mouseScrollDelta.y<0)
        {
            weaponDatas[weaponNumber] = equippedWeapon;
            if (weaponNumber==weaponDatas.Count-1)
            {
                weaponNumber = 0;
            }
            else
            {
                weaponNumber++;
            }
            
            equippedWeapon = weaponDatas[weaponNumber];
            //currentMagAmmo = equippedWeapon.magCapacity;
        }

        if (Input.mouseScrollDelta.y>0)
        {
            weaponDatas[weaponNumber] = equippedWeapon;
            if (weaponNumber==0)
            {
                weaponNumber = weaponDatas.Count - 1;
            }
            else
            {
                weaponNumber--;
            }
            equippedWeapon = weaponDatas[weaponNumber];
            //currentMagAmmo = equippedWeapon.magCapacity;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentMagAmmo != equippedWeapon.magCapacity && equippedWeapon.weaponType != "beam"&&!reloading
            || Input.GetMouseButtonDown(0) && equippedWeapon.currentAmmo == 0 && !reloading && equippedWeapon.weaponType != "beam"&&empty)
        {
            IEnumerator reload()
                {
                reloading = true;
                yield return new WaitForSeconds(equippedWeapon.reloadTime);
                equippedWeapon.currentAmmo = equippedWeapon.magCapacity;
                reloading = false;
                empty = false;
            }
            StartCoroutine(reload());
            
        }

        if (Input.GetMouseButton(0) && equippedWeapon.weaponType == "shotgun" && canFire && equippedWeapon.currentAmmo != 0 && !reloading)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            equippedWeapon.currentAmmo--;
            IEnumerator setEmpty()
            {
                yield return new WaitForSeconds(0.1f);
                empty = true;
            }
            if (equippedWeapon.currentAmmo == 0)
            {
                StartCoroutine(setEmpty());
            }
        }
        else if (Input.GetMouseButton(0) && canFire && equippedWeapon.currentAmmo != 0&&!reloading)
        {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            equippedWeapon.currentAmmo--;
            IEnumerator setEmpty() 
            {
                yield return new WaitForSeconds(0.1f);
                empty = true;
            }
            if (equippedWeapon.currentAmmo==0)
            {
                StartCoroutine(setEmpty());
            }
        }
        
    }
    private void OnLevelWasLoaded(int level)
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
