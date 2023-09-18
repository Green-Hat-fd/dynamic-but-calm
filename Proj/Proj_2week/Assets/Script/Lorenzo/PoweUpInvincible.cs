using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpInvincible : PowerUp
{
    PlayerStatsManager psm;

    public void MakeInvincible()
    {
        StartCoroutine(MakeInvincibleFor());
    }

    private IEnumerator MakeInvincibleFor()
    {
        psm.isDamageable = false;

        yield return new WaitForSeconds(2f);

        psm.isDamageable = true;
    }
}
