using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void OnTriggerEnter2D(Collider2D collision);
    void Die();
}
