using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "GameData/Level")]
public class LevelSO : ScriptableObject
{
    public string    sceneName;
    public Sprite    background;
    public AudioClip music;
    public LevelSO   nextLevel;
}
