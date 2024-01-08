using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager Instance;

    private Coroutine coroutine;

    private Image imageProp;

    private IObjectPool<GameObject> HPBar;
    private GameObject objProp;

    private float time;
    public float TargetTime;

    private void Awake()
    {
        HPBar = PoolManager.Instance.HPBar;
    }

    public void OnHit(Transform tf, Image? image, float totalHP, float targetHP)
    {
        imageProp = tf.GetComponentInChildren<Image>();
        StopCoroutine(coroutine);
        if(imageProp == null)
        {
            SpawnHPBar(tf);
            coroutine = StartCoroutine(HPProgress(imageProp, totalHP, targetHP));
        }          
        else coroutine = StartCoroutine(HPProgress(image, totalHP, targetHP));
    }
    private void SpawnHPBar(Transform tf)
    {
        objProp = HPBar.Get();
        objProp.transform.SetParent(tf);
        objProp.transform.position = Vector3.zero;
        objProp.transform.position += Vector3.up * 1.5f; 
    }
    private IEnumerator HPProgress(Image image, float totalHP, float targetHP)
    {
        image.color = Color.white;
        image.fillAmount = targetHP / totalHP;
        while (time < TargetTime)//아직 프로그레시브 없음
        {
            time -= Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b,  Mathf.Lerp(0, TargetTime, time));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }
}
