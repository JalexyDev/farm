using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionPanel : MonoBehaviour
{
    public RectTransform prefab;
    public RectTransform content;

    public void ShowFunctions(List<Function> functions)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var function in functions)
        {
            var instance = Instantiate(prefab.gameObject);
            instance.transform.SetParent(content, false);
            InitializeFuncHolder(instance, function);
        }
    }

    public void Close()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
    }

    private void InitializeFuncHolder(GameObject holderObj, Function function)
    {
        FunctionPanelHolder holder = holderObj.GetComponent<FunctionPanelHolder>();
        holder.Icon.sprite = function.Information.Icon;
        holder.Name.text = function.Information.Name;
        holderObj.GetComponent<Button>().onClick.AddListener(() =>
        {
            function.Action();
        });

        //todo Добавить всплывашку с описанием при наведении на элемент (или удержании пальца)
    }
}
