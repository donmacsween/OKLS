using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Base : MonoBehaviour
{
    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_Clip1;
    [SerializeField] private AudioClip m_Clip2;
                     private bool isAlternating = false;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // prefiltered to enemy layer
        // Tell the BaseManager.cs to deduct health equivelent to enemy's baseDamage
        BaseManager.Instance.DeductHealth(other.gameObject.GetComponent<Enemy>().baseDamage);
        SpawnManager.Instance.DespawnEnemy(other.GetComponent<Enemy>());
        m_AudioSource.loop = false;
        if (isAlternating)
        {
            m_AudioSource.PlayOneShot(m_Clip1);
        }
        else
        {
            m_AudioSource.PlayOneShot(m_Clip1);
        }
        
        

    }
}
