
using UnityEngine;
using UnityEngine.UI;

public class DialogNext : MonoBehaviour
{

    private Button button;
    public DialogSO nextDialog;
    [SerializeField] private DialogPanel panel;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(DialogButtonClicked);
    }

    private void DialogButtonClicked()
    {
        panel.currentDialog = nextDialog;
        panel.ShowDialog();
    }
}
