using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public GameObject normal;
    public GameObject fast;
    public GameObject lourd;
    public Vector3 spawnPoint = new Vector3(0, 0, 0);
    public GameObject panelSelection;
    public Transform parent;
    public Transform currentPlayer;
    public GameObject player;
    public Image backgroundImageHealth;
    public Image foreGroundImageHealth;
    public GameObject uiHealthBar;
    public GameObject uiIconSkills;
    public Image iconSkillLeftClick;
    public Image iconSkillRightClick;
    public Image iconSkillA;
    public Image iconSkillE;

    public TextMeshProUGUI ammo;

    public void SetHealthBarPercentagePlayer(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foreGroundImageHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
    public void SelectCharacter(GameObject name)
    {
        GameObject player = Instantiate(name, spawnPoint, Quaternion.identity, parent);
        panelSelection.SetActive(false);
        currentPlayer = player.transform;
        uiHealthBar.SetActive(true);
        uiIconSkills.SetActive(true);
        
    }

    public GameObject objectToSpawn;
    public Vector3 spawnPosition;
    public float spawnInterval = 5f;
    public Transform enemyParent;
    private float lastSpawnTime = 0f;

    void Update()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, enemyParent);
            lastSpawnTime = Time.time;
        }
        
    }
    
}
