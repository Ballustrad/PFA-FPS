using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    
    #region Singleton

    private static CharacterData instance;

    public static CharacterData Instance
    {
        get
        {
            instance = instance ?? Resources.Load<CharacterData>("ManagerSingleto,/CharacterData");
            Debug.Log(instance);
            return instance;
        }
    }
    #endregion


    public float speed;
    public float jumpHeight;

    public float health;
    public Image iconSkillLeftClick;
    public Image iconSkillRightClick;
    public Image iconSkillA;
    public Image iconSkillE;

}
