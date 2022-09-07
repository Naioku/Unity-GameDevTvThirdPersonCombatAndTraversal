using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponDamageCollider;

    public void ActivateWeaponDamage()
    {
        weaponDamageCollider.SetActive(true);
    }

    public void DeactivateWeaponDamage()
    {
        weaponDamageCollider.SetActive(false);
    }
}
