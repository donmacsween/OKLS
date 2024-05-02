using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogPanel : MonoBehaviour
{
                     public  DialogSO   currentDialog;
    [SerializeField] private TMP_Text   textField;
    [SerializeField] private Image      image;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject nextDialogButton;
    [SerializeField] private DialogNext nextDialog;

    private void OnEnable()
    {
       
            ShowDialog();
    }

    public void ShowDialog()
    {
        textField.text = currentDialog.characterDialog;
        image.sprite = currentDialog.character;

        if (currentDialog.isEndOfDialog)
        {
            startGameButton.SetActive(true);
            nextDialogButton.SetActive(false);
        }
        else
        {
            nextDialog.nextDialog = currentDialog.nextDialog;
            startGameButton.SetActive(false);
            nextDialogButton.SetActive(true);
        }
    }
}
