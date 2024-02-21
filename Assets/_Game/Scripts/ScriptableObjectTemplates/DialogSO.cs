
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "GameData/Dialog")]
public class DialogSO : ScriptableObject
{
    public string   characterDialog           ="";
    public Sprite   character;
    public DialogSO nextDialog;
    public bool     isEndOfDialog;
}
