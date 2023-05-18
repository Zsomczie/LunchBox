using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class WeaponImage : MonoBehaviour
{
    [SerializeField] GameObject weapon, weapon1, weapon2, aimer;
    Shooting shooting;
    [SerializeField] SpriteLibrary spriteLibrary;
    // Start is called before the first frame update
    void Start()
    {
        shooting = GameObject.Find("RotatePoint").GetComponent<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {
        weapon.GetComponent<Image>().sprite = aimer.GetComponent<SpriteLibrary>().spriteLibraryAsset.GetSprite("Weapons", shooting.equippedWeapon.weaponName);
        //weapon1.GetComponent<Image>().sprite = aimer.GetComponent<SpriteLibrary>().spriteLibraryAsset.GetSprite("Weapons", shooting.weaponDatas[shooting.weaponNumber - 1].weaponName);
        //weapon2.GetComponent<Image>().sprite = aimer.GetComponent<SpriteLibrary>().spriteLibraryAsset.GetSprite("Weapons", shooting.weaponDatas[shooting.weaponNumber+1].weaponName);
        //weapon2.GetComponent<Image>().sprite = aimer.GetComponent<SpriteRenderer>().sprite;
    }
}
