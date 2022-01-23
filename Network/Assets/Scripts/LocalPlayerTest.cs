using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerTest : MonoBehaviour
{
    private void Awake()
    {
        Timing.RunCoroutine(Wait());
    }

    private void Start()
    {

    }

    private IEnumerator<float> Wait()
    {
        while (true)
        {
            Debug.Log(Time.frameCount);
            yield return Timing.WaitForSeconds(0.5f);
        }
    }
}