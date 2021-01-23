
//todo обрабатывать нажатие кнопки "Удалить"

using UnityEngine;

public class BedsPlantPreview : Preview
{
    private void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().material.color = new Color(0, 1f, .3f, .8f);
    }

    private void Start()
    {
        Controls.OpenMenuDisabled = true;
    }

    private void OnDestroy()
    {
        Controls.OpenMenuDisabled = false;
    }
}
