using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "GameData/Level")]
public class LevelSO : ScriptableObject
{
    public string       sceneName;
    public string       sceneLable;
    public Sprite       background;
    public AudioClip    music;
    public LevelSO      nextLevel;
    public int          startingGold;
    public int          startingHealth;
    public WaveSO[]     waves;
    public DialogSO     startingDialog;
}
