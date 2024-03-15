
using UnityEngine;
using UnityEngine.UI;

public class ClickableMap : MonoBehaviour
{
    public float alphaThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

   
}
