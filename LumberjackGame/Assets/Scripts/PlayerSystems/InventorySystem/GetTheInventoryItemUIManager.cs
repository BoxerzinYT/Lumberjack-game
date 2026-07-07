using UnityEngine;

public class GetTheInventoryItemUIManager : MonoBehaviour
{
    [SerializeField] InventoryItemUIManager inventItemUIMan;
    public InventoryItemUIManager InventItemUIMan { get { return inventItemUIMan; } }
}
