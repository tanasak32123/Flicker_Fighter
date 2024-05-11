using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UiManagerSc : MonoBehaviour
{
    public TextMeshProUGUI turnLabel;
    public TextMeshProUGUI plaLabel;
    public TextMeshProUGUI charLabel;
    public TextMeshProUGUI velLabel;
    [SerializeField] private CharSelectorSc characterSelector;

    // Start is called before the first frame update
    void Start()
    {
        //turnLabel.text = "Turn : -";
        //plaLabel.text = "[Player : -] [-/-]";
        //charLabel.text = "Type : -";
        //velLabel.text = "HP : -\nVelocity : -";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateTurnUi(int turn,int player, int curr, int count, string name)
    {
        player = player + 1;
        turnLabel.text = "Turn : "+ turn.ToString();
        plaLabel.text = "[Player : " + player.ToString() + "] [" + curr.ToString() +"/" + count.ToString() + "]";
        charLabel.text = "Type : "+ name;
    }

    public void updateRtUi(float hp, float atk, float vel)
    {
        velLabel.text = "HP : " + hp.ToString() +"\nATK : " + atk.ToString() +"\nVelocity : "+ vel.ToString("F2");
    }
}
