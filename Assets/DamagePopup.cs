using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Diagnostics;


public class DamagePopup : MonoBehaviour
{

    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int sortingOrder;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    // Start is called before the first frame update

    public static DamagePopup Create(Vector3 position,int damageAmount,bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount,isCriticalHit);

        return damagePopup;
    }
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }


    public void Setup(int damageAmount,bool isCriticalHit)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        if (!isCriticalHit)
        {
            textMesh.fontSize = 12;
            textColor = Color.yellow;
        }
        else
        {
            textMesh.fontSize = 24;
            textColor = Color.red;

        }
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(.7f, 1) * 60f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmont = 1f;
            transform.localScale += Vector3.one * increaseScaleAmont * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;





        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
