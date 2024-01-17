using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager Instance;

    private Coroutine coroutine;

    private Image[] imageProps;

    private IObjectPool<GameObject> HPBar;
    private GameObject objProp;

    public float TargetTime;
    private float time;

    public GameObject InfoGroup;
    public Text HpText;
    public Text DamageText;
    
    private void Awake()
    {
        Instance = this;
        HPBar = PoolManager.Instance.HPBar;
    }

    public void OnHit(Transform tf, float totalHP, float targetHP)
    {
        imageProps = tf.GetComponentsInChildren<Image>();
        if(coroutine != null)
            StopCoroutine(coroutine);
        if(imageProps.Length == 0)
        {
            SpawnHPBar(tf);
            imageProps = tf.GetComponentsInChildren<Image>();
            if(imageProps.Length > 0)
                coroutine = StartCoroutine(HPProgress(imageProps, totalHP, targetHP));
        }          
        else coroutine = StartCoroutine(HPProgress(imageProps, totalHP, targetHP));
    }
    private void SpawnHPBar(Transform tf)
    {
        objProp = HPBar.Get();
        objProp.transform.SetParent(tf);
        objProp.transform.localPosition = Vector3.up * 1.5f;
    }
    private IEnumerator HPProgress(Image[] image, float totalHP, float targetHP)
    {
        time = 0;
        for(int i = 0; i < image.Length; i++) 
        {
            image[i].color = Color.white;
            image[i].fillAmount = targetHP / totalHP;
        }
        while (time < TargetTime)//아직 프로그레시브 없음
        {
            time += Time.deltaTime;
            for (int i = 0; i < image.Length; i++)    
                image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b,  1 - Mathf.Lerp(0, TargetTime, time));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        for (int i = 0; i < image.Length; i++)
            image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, 0);
        coroutine = null;
    }
}
