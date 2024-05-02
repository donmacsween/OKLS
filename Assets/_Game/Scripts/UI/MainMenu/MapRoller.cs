
using UnityEngine;

public class MapRoller : MonoBehaviour
{
    Renderer rend;
    [SerializeField] private float      closedRollValue = 0.065f;
    [SerializeField] private float      openRollValue   = 1.0f;
    [SerializeField] private float      rollSpeed       = 0.1f;
    [SerializeField] private float      rollValue;
    [SerializeField] private GameObject mapButtons;
                     private bool        unrolling       = true;
    void OnEnable()
    {
#if UNITY_EDITOR
        rollSpeed = 0.6f;
#endif

        rend = GetComponent<Renderer>();
        rollValue = closedRollValue;
        rend.material.shader = Shader.Find("Shader Graphs/MapRollDoubleSidedURP");
        mapButtons.SetActive(false);
        rend.material.SetFloat("Vector1_98d33b1d219b486e97f4a6d459a007a3", closedRollValue);
    }
    void Update()
    {       
           if (rollValue >= closedRollValue && unrolling==false)
           {
                rollValue -= rollSpeed * Time.deltaTime;
                rend.material.SetFloat("Vector1_98d33b1d219b486e97f4a6d459a007a3", rollValue);
           }
            
        if ((rollValue < openRollValue) && unrolling == true)
        {
                rollValue += rollSpeed*Time.deltaTime;
                rend.material.SetFloat("Vector1_98d33b1d219b486e97f4a6d459a007a3", rollValue);
        }
        if (rollValue >= 1) {mapButtons.SetActive(true);}
        if (!unrolling) {mapButtons.SetActive(false);}
    }
    public void Rollup()
    {
        unrolling = false;

    }
    public void UnRoll()
    {
        unrolling = true;
    }
}