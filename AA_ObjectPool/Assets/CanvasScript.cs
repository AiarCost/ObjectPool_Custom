using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private void OnEnable()
    {
        ObjectPool.OnNoMoreEnemies += TurnOnText;
    }

    public GameObject textGO;
    public void TurnOnText()
    {
        textGO.SetActive(true);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        textGO.SetActive(false);
    }
}
