using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiveBarrel : InteractionObject
{
    [Header("Destructible Barrel")]
    [SerializeField]
    private GameObject  destructibleBarrelPieces;

    private bool        isDestroyed = false;

    public override void TakeDamage(int damage)
    {
        currentHP -= damage;

        if ( currentHP <= 0 && isDestroyed == false )
        {
            isDestroyed = true;

            Instantiate(destructibleBarrelPieces, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

}
