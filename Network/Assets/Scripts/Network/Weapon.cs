using MEC;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem startShotEffect;
    [SerializeField]
    private LineRenderer shotEffect;

    [SerializeField]
    private float shotEffectDelay;

    public void StartShot()
    {
        startShotEffect.Play();
    }

    public void Shot(Vector2 targetPosition)
    {
        Timing.RunCoroutine(ShotEffectDelay(targetPosition));
    }

    private IEnumerator<float> ShotEffectDelay(Vector2 targetPosition)
    {
        shotEffect.enabled = true;

        shotEffect.SetPosition(0, transform.position);
        shotEffect.SetPosition(1, targetPosition);

        yield return Timing.WaitForSeconds(shotEffectDelay);
        shotEffect.enabled = false;
    }
}