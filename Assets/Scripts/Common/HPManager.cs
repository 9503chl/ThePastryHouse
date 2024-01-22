using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager Instance;

    private List<Image[]> imagesList = new List<Image[]>();
    
    private Image[] imageProps = new Image[2];

    private IObjectPool<GameObject> HPBar;
    private GameObject objProp;

    public float Speed;

    public GameObject InfoGroup;
    public Text HpText;

    private void Awake()
    {
        Instance = this;
        HPBar = PoolManager.Instance.HPBarPool;
    }
    private void OnEnable()
    {
        StartCoroutine(HPProgress());
    }

    public void OnHit(Transform tf, float totalHP, float targetHP)
    {
        Image[] getImages = tf.GetComponentsInChildren<Image>();

        if (getImages.Length == 1 || getImages.Length == 0)
        {
            SpawnHPBar(tf);
            getImages = tf.GetComponentsInChildren<Image>();
        }
        
        if (getImages.Length == 3)
        {
            imageProps[0] = getImages[1];
            imageProps[1] = getImages[2];
        }
        else
        {
            imageProps = getImages;
        }
        for (int i = 0; i < imageProps.Length; i++)
            imageProps[i].color = Color.white;
        imageProps[imageProps.Length - 1].fillAmount = targetHP / totalHP;
        if(!imagesList.Contains(imageProps))
            imagesList.Add(imageProps);
    }
    private void SpawnHPBar(Transform tf)
    {
        objProp = HPBar.Get();
        objProp.transform.SetParent(tf);
        objProp.transform.localPosition = Vector3.up * 1.5f;
    }
    private IEnumerator HPProgress()
    {
        while (isActiveAndEnabled)
        {
            for (int i = 0; i < imagesList.Count; i++)
                for (int j = 0; j < imagesList[i].Length; j++)
                {
                    if (imagesList[i][imagesList[i].Length - 1].color.a == 0)
                        imagesList.Remove(imagesList[i]);
                    imagesList[i][j].color = new Color(imagesList[i][j].color.r, imagesList[i][j].color.g, imagesList[i][j].color.b, imagesList[i][j].color.a - Speed);
                }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
