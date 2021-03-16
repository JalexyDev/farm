using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//todo при нажатии мыши в ее позиции кстить круг и проверять пересечения
//если пересекается с Iclickable в зависимости от преоритета вызвать у нужного объекта OnClick()
public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.CircleCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f, Vector2.one);

            if (hit.collider == null) { return; }

            IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }

        //todo добавить обработку других клавиш мыши и жестов пальцами
    }
}
