using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    /*
     ����ģʽ
     */
    public static InventoryManager instance;

    public Inventory playerBag;         //��ұ���
    public GameObject InventorySlotGrid;        //��������
    public Slot slotPrefab;         //�����Ӷ���
    public Text InventorySlotInfo;      //��Ʒ����
    public RawImage InventorySlotImage;     //��Ʒͼ��
    public InventoryMenu Menu;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void Start()
    {
        instance.playerBag.itemList.Clear();        //��ʼ������
        RefreshItem();
    }

    private void OnEnable()     //��ʼ״̬չʾ��ǰ�����б���Ʒ
    {

        //for (int i = 0; i < instance.playerBag.itemList.Count; i++)
        //{
        //    if (instance.playerBag.itemList[i] != null)     //�����Ϊ�յĻ�������ͼƬ����չʾ��ʼ��Ϊ�����б��һ��Ԫ��
        //    {
        //        instance.InventorySlotInfo.text = instance.playerBag.itemList[i].itemInfo;
        //        instance.InventorySlotImage.texture = instance.playerBag.itemList[i].itemImage;
        //        break;
        //    }
        //    else   //���Ϊ�ա�����ͼƬ����չʾ��ʾΪ�հ�
        //    {
        instance.InventorySlotInfo.text = "";
        instance.InventorySlotImage.texture = null;
        //    }
        //}
    }

    /// <summary>
    /// ��Ʒ�����¼�
    /// </summary>
    /// <param name="_ItemDescription">��������</param>
    public static void UpdateItemInfo(string _ItemDescription, Texture _ItemImage, int _Number)
    {
        instance.InventorySlotInfo.text = _ItemDescription;
        instance.InventorySlotImage.texture = _ItemImage;

        instance.Menu.SlotNumber = _Number;
    }

    /// <summary>
    /// ����������Ʒ�¼�
    /// </summary>
    /// <param name="_item">��Ʒ</param>
    public static void CreateNewItem(Item _item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.InventorySlotGrid.transform.position, Quaternion.identity);        //ʵ����
        newItem.gameObject.transform.SetParent(instance.InventorySlotGrid.transform, false);       //��������Ʒ�ĸ���
        newItem.slotItem = _item;
        newItem.slotName.text = _item.itemName;
        newItem.slotNumber.text = "��" + _item.itemNumber.ToString();
    }

    /// <summary>
    /// ���±����¼�
    /// </summary>
    public static void RefreshItem()
    {
        //������������Ӷ���
        for (int i = 0; i < instance.InventorySlotGrid.transform.childCount; i++)
        {
            if (instance.InventorySlotGrid.transform.childCount == 0) break;
            Destroy(instance.InventorySlotGrid.transform.GetChild(i).gameObject);
        }

        //ɨ�豳���б���������б��Ӷ���
        for (int i = 0; i < instance.playerBag.itemList.Count; i++)
        {
            CreateNewItem(instance.playerBag.itemList[i]);
        }
    }
}
