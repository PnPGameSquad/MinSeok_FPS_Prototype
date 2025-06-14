using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private WeaponAssultRifle   weapon;
    [SerializeField]
    private Status              status;

    [Header("Weapon Base")]
    [SerializeField]
    private TextMeshProUGUI     textWeaponName;
    [SerializeField]
    private Image               imageWeaponIcon;
    [SerializeField]
    private Sprite[]            spriteWeaponIcons;

    [Header("Ammo")]
    [SerializeField]
    private TextMeshProUGUI     textAmmo;

    [Header("Magazine")]
    [SerializeField]
    private GameObject          magazineUIPrefab;
    [SerializeField]
    private Transform           magazineParent;

    private List<GameObject>    magazineList;

    [Header("HP & BloodScreen UI")]
    [SerializeField]
    private TextMeshProUGUI     textHP;
    [SerializeField]
    private Image               imageBloodScreen;
    [SerializeField]
    private AnimationCurve      curveBloodScreen;

    private void Awake()
    {
        SetupWeapon();
        SetupMagazine();

        weapon.onAmmoEvent.AddListener(UpdateAmmoHUD);
        weapon.onMagazineEvent.AddListener(UpdateMagazineHUD);
        status.onHPEvent.AddListener(UpdateHPHUD);
    }

    private void SetupWeapon()
    {
        textWeaponName.text = weapon.WeaponName.ToString();
        imageWeaponIcon.sprite = spriteWeaponIcons[(int)weapon.WeaponName];
    }

    private void UpdateAmmoHUD(int currentAmmo, int maxAmmo)
    {
        textAmmo.text = $"<size=40>{currentAmmo}/</size>{maxAmmo}";
    }

    private void SetupMagazine()
    {
        magazineList = new List<GameObject>();
        for ( int i = 0; i < weapon.MaxMagazine; i++ )
        {
            GameObject clone = Instantiate(magazineUIPrefab);
            clone.transform.SetParent(magazineParent);
            clone.SetActive(false);

            magazineList.Add(clone); 
        }

        for ( int i = 0; i< weapon.CurrentMagazine; ++ i )
        {
            magazineList[i].SetActive(true);
        }
    }

    private void UpdateMagazineHUD(int currentMagazine)
    {
        for ( int i = 0; i < magazineList.Count; ++i )
        {
            magazineList[i].SetActive(false);
        }
        for ( int i = 0; i < currentMagazine; ++ i )
        {
            magazineList[i].SetActive(true);
        }
    }

    private void UpdateHPHUD(int previous, int current)
    {
        textHP.text = "HP"+current;

        if ( previous - current > 0 )
        {
            StopCoroutine("OnBloodScreen");
            StartCoroutine("OnBloodScreen");
        }
    }

    private IEnumerator OnBloodScreen()
    {
        float percent = 0;

        while ( percent < 1 )
        {
            percent += Time.deltaTime;

            Color color             = imageBloodScreen.color;
            color.a                 = Mathf.Lerp(1, 0, curveBloodScreen.Evaluate(percent));
            imageBloodScreen.color  = color;

            yield return null;
        }
    }
}
