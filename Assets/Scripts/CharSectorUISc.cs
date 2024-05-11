using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSectorUISc : MonoBehaviour
{
    public List<Player> playerOptions = new List<Player>();
    public CharSelectorSc charSelectorSc;
    public Sprite[] characterImages;
    public Transform contentContainer;
    public GameObject buttonPrefab;
    public TextMeshProUGUI playerlabel;
    public TextMeshProUGUI p1Playerlabel;
    public TextMeshProUGUI p2Playerlabel;
    public Transform p1PlayerContainer;
    public Transform p2PlayerContainer;

    void Start()
    {
        init_select_btns();
    }


    private void init_select_btns()
    {
        for(int i = 0;i < playerOptions.Count;i++)
        {
            Player player = playerOptions[i];
            Sprite image = characterImages[i];
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(contentContainer);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            button.GetComponent<Image>().sprite = image;
            button.GetComponent<Button>().onClick.AddListener(() => {
                charSelectorSc.selectChar(player,image);
                fetch_char_label(0);
                fetch_char_label(1);
            });
        }
    }

    public void fetch_char_label(int player)
    {
        if(player == 0)
        {
            foreach (Transform child in p1PlayerContainer)
            {
                Destroy(child.gameObject);
            }
        }
        else
        {
            foreach (Transform child in p2PlayerContainer)
            {
                Destroy(child.gameObject);
            }
        }
        var charSprites = charSelectorSc.getAllCharSprite(player);
        foreach (Sprite charSprite in charSprites)
        {
            GameObject imgObject = new GameObject("test");
            Image img = imgObject.AddComponent<Image>();
            img.sprite = charSprite;
            if (player == 0)
            {
                imgObject.transform.SetParent(p1PlayerContainer);
                continue;
            }
            imgObject.transform.SetParent(p2PlayerContainer);
        }

    }

    public void fetch_current_player()
    {
        playerlabel.text = "Player : " + (charSelectorSc.getCurrent() +1).ToString();
    }
}
