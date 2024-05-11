using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Diagnostics;


public class TextPopup : MonoBehaviour
{

    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int sortingOrder;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    // Start is called before the first frame update

    public static TextPopup Create(Vector3 position, string text,Color color)
    {
        Transform textPopupTransform = Instantiate(GameAssets.i.pfTextPopup, position, Quaternion.identity);
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.Setup(text,color);

        return textPopup;
    }
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }


    public void Setup(string text,Color color)
    {
        textMesh.text = text;
        textColor = color;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        textMesh.fontSize = 24;
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
