using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager Instance;

    private Dictionary<string, Image[]> imageDictionary = new Dictionary<string, Image[]>();
    private List<string> nameList = new List<string>();
    private Image[] imageProps = new Image[2];

    public IObjectPool<GameObject> HPPool;

    public List<GameObject> HPPoolList = new List<GameObject>();

    private GameObject objProp;

    public float Speed;

    public GameObject InfoGroup;
    public Text HpText;

    private void Awake()
    {
        Instance = this;
        HPPool = PoolManager.Instance.HPBarPool;
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
        if (!imageDictionary.ContainsKey(tf.name))
        {
            imageDictionary.Add(tf.name, imageProps);
            nameList.Add(tf.name);
        }
    }
    private void SpawnHPBar(Transform tf)
    {
        objProp = HPPool.Get();
        HPPoolList.Add(objProp);
        objProp.transform.SetParent(tf);
        objProp.transform.localPosition = Vector3.up * 1.5f;
    }
    private IEnumerator HPProgress()
    {
        while (isActiveAndEnabled)
        {
            for (int i = 0; i < imageDictionary.Count; i++)
            {
                Image[] tempImages = imageDictionary[nameList[i]];
                for (int j = 0; j < tempImages.Length; j++)
                {
                    if (tempImages[tempImages.Length - 1].color.a == 0)
                    {
                        imageDictionary.Remove(nameList[i]);
                        nameList.RemoveAt(i);
                    }
                    tempImages[j].color = new Color(tempImages[j].color.r, tempImages[j].color.g, tempImages[j].color.b, tempImages[j].color.a - Speed);
                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitUntil(() => nameList.Count > 0);
        }
    }
}
