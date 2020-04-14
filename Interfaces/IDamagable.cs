using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int CollideDamage { get; set; }

    void onCollisionrEnter2D(Collision2D collision);
}
