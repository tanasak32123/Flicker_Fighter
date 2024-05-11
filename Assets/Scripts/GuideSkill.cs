using UnityEngine;

public class GuideSkill : MonoBehaviour
{
    private GameObject GuideSkillUI;
    private Player player;

    void Start() {
        GuideSkillUI = GameObject.Find("/Canvas/GuideSkill/ClickingSkill");
        player = GetComponent<Player>();
        GuideSkillUI.SetActive(false);
    }

    void Update() {
        if (player.isTurn) {
            if (player.IsActivateAbility) {
                if (!GuideSkillUI.activeInHierarchy) GuideSkillUI.SetActive(true);
            } else {
                if (GuideSkillUI.activeInHierarchy) GuideSkillUI.SetActive(false);
            }
        }
    }
}