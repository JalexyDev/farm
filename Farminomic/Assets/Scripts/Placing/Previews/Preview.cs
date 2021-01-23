using UnityEngine;

public class Preview : MonoBehaviour
{
    private Information placableInfo;
    private int placablePrice;
    private bool isProcessing;

    public int PlacablePrice { get => placablePrice; set => placablePrice = value; }
    public Information PlacableInfo { get => placableInfo; set => placableInfo = value; }
    
    //когда запрос летит на сервер превьюха "Замораживается"
    public bool IsProcessing { get => isProcessing; set => isProcessing = value; }
}
