using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnlockingSkill : MonoBehaviour
{
    
    public float costSkill1;
    public float costSkill2;
    public float costSkill3;
    [Space]
    public bool skillUnlocked1 = false;
    public bool skillUnlocked2 = false;
    public bool skillUnlocked3 = false;
    [Space]
    public MiddleManScenehandler middleManScenehandler;
    [Space]
    public Image Holder1;
    public Image Holder2;
    public Image Holder3;
    [Space]
    public TextMeshProUGUI skillName1;
    public TextMeshProUGUI skillName2;
    public TextMeshProUGUI skillName3;

    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI txt3;
    [Space]
    public Sprite skillNormal1;
    public Sprite skillNormal2;
    public Sprite skillNormal3;
    [Space]
    public Sprite skillFast1;
    public Sprite skillFast2;
    public Sprite skillFast3;
    [Space]
    public Sprite SkillHeavy1;
    public Sprite SkillHeavy2;
    public Sprite SkillHeavy3;

    public ScoreManager scoreManager;
    public GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        scoreManager = ScoreManager.Instance;   

        
    }

    private void OnEnable()
    {
        string selectedPlayer = PlayerPrefs.GetString("SelectedPlayer", "fast");

        switch (selectedPlayer)
        {
            case "fast":
                Holder1.sprite = skillFast1;
                Holder2.sprite = skillFast2;
                Holder3.sprite = skillFast3;
                txt1.text = "Instantly recover 20% of your life points (Cooldown: 12 seconds)";
                txt2.text = "Paralyze all enemies in your field of vision for 2,5 seconds (Cooldown: 20 seconds)";
                txt3.text = "Perform a forward dash (Cooldown: 5 seconds)";
                skillName1.text = "Harmonic Heal: "+ costSkill1 + " Points.";
                skillName2.text = "Fantastic Freeze: " + costSkill2 + " Points.";
                skillName3.text = "Dashing Dash: " + costSkill3 + " Points.";
                break;
            case "normal":
                Holder1.sprite = skillNormal1;
                Holder2.sprite = skillNormal2;
                Holder3.sprite = skillNormal3;
                txt1.text = "Launch a stunt grendade forward that explodes on impact, stunning enemies for 1,5 seconds (Cooldown: 9 seconds)";
                txt2.text = "Grant yourself an armor boost for 5 seconds, reducing damaged received by 20% (Cooldown: 15 seconds)";
                txt3.text = "Apply a mark on targeted enemy, increasing the damage he takes by 25% (Cooldown: 7 seconds)";
                skillName1.text = "Splendid Stun: " + costSkill1 + " Points.";
                skillName2.text = "Adorable Armor: " + costSkill2 + " Points.";
                skillName3.text = "Smilly Sigil: " + costSkill3 + " Points.";
                break;
            case "heavy":
                Holder1.sprite = SkillHeavy1;
                Holder2.sprite = SkillHeavy2;
                Holder3.sprite = SkillHeavy3;
                txt1.text = "Incresae your fire rate by 25% and reload Speed by 50% for 5 seconds (Cooldown: 15 seconds)";
                txt2.text = "Hit the ground and unleash a shockwave that stuns nearby enemies for 2 seconds and deals 40 damage (Cooldown: 13 Seconds )";
                txt3.text = "Fire a missile that explodes on impact and inflict 75 damage to the ennemies (Cooldown: 7 seconds)";
                skillName1.text = "Radiant Rate: " + costSkill1 + " Points.";
                skillName2.text = "Sweet Stomp: " + costSkill2 + " Points.";
                skillName3.text = "Majestic Missile: " + costSkill3 + " Points.";
                break;
            default:
                return;

        }

    }

    public void SkillUnlocked(string skillSlot)
    {
        switch (skillSlot)
        {
            case "Skill 1":
                if (skillUnlocked1 == false && costSkill1 < middleManScenehandler.currentPoint)
                {
                    skillUnlocked1 = true;
                    middleManScenehandler.currentPoint -= costSkill1;
                    scoreManager.UpdateScoreTextTotal();
                    middleManScenehandler.canUseSkill1 = true;
                    skillName1.text = "Unlocked";
                    
                }
                break;
            case "Skill 2":
                if (skillUnlocked2 == false && costSkill2 < middleManScenehandler.currentPoint)
                {
                    skillUnlocked2 = true;
                    middleManScenehandler.currentPoint -= costSkill2;
                    scoreManager.UpdateScoreTextTotal();
                    middleManScenehandler.canUseSkill2 = true;
                    skillName2.text = "Unlocked";
                }
                break;
            case "Skill 3":
                if (skillUnlocked3 == false && costSkill3 < middleManScenehandler.currentPoint)
                {
                    skillUnlocked3 = true;
                    middleManScenehandler.currentPoint -= costSkill2;
                    scoreManager.UpdateScoreTextTotal();
                    middleManScenehandler.canUseSkill3 = true;
                    skillName3.text = "Unlocked";
                }
                break;

        }
        
        
    }
 




}
