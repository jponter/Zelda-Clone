using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : GenericDamage
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update



}
