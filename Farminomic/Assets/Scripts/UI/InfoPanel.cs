using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Text Title;
    public Image Icon;
    public Text Description;

    public Sprite Empty;

    public void SetInformation(Information information)
    {
        SetTitle(information.Name);
        SetIcon(information.Icon);
        SetDescription(information.Description);
    }

    public void Close()
    {
        SetTitle("???");
        SetDescription("???");
        SetIcon(Empty);

        gameObject.SetActive(false);
    }

    private void SetTitle(string titleText)
    {
        Title.text = titleText;
    }

    private void SetIcon(Sprite sprite)
    {
        Icon.sprite = sprite;
    }

    private void SetDescription(string descrText)
    {
        Description.text = descrText;
    }
}
