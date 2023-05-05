using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu instance;
    public CameraController camcon;
    public int SlotNumber;
    public int MoneyNumber;      //����
    public Text MoneyShow;
    public Item item;       //��ǰ��Ʒ
    public ItemOnWorld RightHalfLanting;        //�Ұ����ͤ��
    public ItemOnWorld False_Lanting;     //����ͤ��
    public KeyBoardInput KBI;
    public GameObject BlurImage_Bag;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }
    private void Start()
    {
        MoneyNumber = 20;
        item = new Item();

        MoneyShow.text = MoneyNumber.ToString();
    }
    public void Update()
    {
        print(SlotNumber);
        if (Input.GetKeyDown("v"))      //�鿴��ǰ��Ʒ
        {
            if (item.check)
            {

                ItemCheck();
                camcon.isOpen = false;      //���ر���
                camcon.BagSystem.SetActive(camcon.isOpen);

                camcon.ShowNumber = SlotNumber;  //�˳��鿴״̬

                camcon.BlurImage_Bag.SetActive(false);      //����������ʧ
                camcon.BlurImage_ThingShow.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver); //��Ʒ������ʾ
            }
            else
            {
                camcon.Hint_Interact.text = "����Ʒ���ɲ鿴��";   //��̬������Ʒ��ʾ��Ϣ
                camcon.Hint_Interact.enabled = true;
            }
        }
        else if (Input.GetKeyDown("g"))         //���۵�ǰ��Ʒ
        {
            if (item.sell)
            {
                ItemSell();
                MoneyShow.text = MoneyNumber.ToString();     //���µ�ǰ��������
                camcon.Hint_Interact.text = item.itemName + "���۳ɹ���";   //��̬������Ʒ��ʾ��Ϣ
                camcon.Hint_Interact.enabled = true;
            }
            else
            {
                camcon.Hint_Interact.text = "����Ʒ���ɳ��ۣ�";   //��̬������Ʒ��ʾ��Ϣ
                camcon.Hint_Interact.enabled = true;
            }
        }
        else if (Input.GetKeyDown("r"))         //������ǰ��Ʒ
        {
            if (item.discard)
            {
                ItemDiscard();
                camcon.Hint_Interact.text = item.itemName + "�Ѷ�����";   //��̬������Ʒ��ʾ��Ϣ
                camcon.Hint_Interact.enabled = true;
                InventoryManager.instance.InventorySlotInfo.text = "";       //�ı�����Ϊ��
                InventoryManager.instance.InventorySlotImage.texture = null;        //ͼƬչʾΪ��
            }
            else
            {
                camcon.Hint_Interact.text = "����Ʒ���ɶ�����";   //��̬������Ʒ��ʾ��Ϣ
                camcon.Hint_Interact.enabled = true;
            }
        }
        else if (Input.GetKeyDown("h"))     //�ϲ���ǰ��Ʒ
        {
            if (item.merge)
            {
                ItemMerge();
            }
            else
            {
                camcon.Hint_Interact.text = "����Ʒ���ɺϲ���";   //��̬������Ʒ��ʾ��Ϣ
                camcon.Hint_Interact.enabled = true;
            }
        }
    }
    /// <summary>
    /// ��Ʒ�鿴�¼�
    /// </summary>
    public void ItemCheck()
    {
        switch (SlotNumber)
        {
            case 1:
                camcon.Half_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                break;
            case 2:
                camcon.LiHe_Letter.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                break;
            case 3:
                camcon.Poes.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                break;
            case 24:        //�鿴����ͤ��
                camcon.False_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                camcon.ShowNumber = 24;     //�˳��鿴״̬
                break;
            case 25:        //�鿴����ͤ��
                camcon.False_Lanting.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
                camcon.ShowText.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);

                camcon.ShowNumber = 25;     //�˳��鿴״̬

                break;
            default:
                break;
        }
    }
    /// <summary>
    /// ��Ʒ�����¼�
    /// </summary>
    public void ItemSell()
    {
        for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
        {
            if (SlotNumber == InventoryManager.instance.playerBag.itemList[i].Number)
            {
                if (item.itemNumber > 1)        //��Ʒ��������1���Լ�1
                {
                    item.itemNumber -= 1;
                }
                else  //��Ʒ��������1�����ٱ���ϵͳ��ǰ��Ʒ
                {
                    InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                }
            }
        }
        InventoryManager.RefreshItem();     //ˢ�±���
        MoneyNumber += 5;        //��������5

    }
    /// <summary>
    /// ��Ʒ�ϲ��¼�
    /// </summary>
    public void ItemMerge()
    {
        for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
        {
            if (SlotNumber == InventoryManager.instance.playerBag.itemList[i].Number)
            {
                if (item.itemNumber == 4)
                {
                    InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                    RightHalfLanting.AddNewItem();

                    camcon.Hint_Interact.text = "��ð�����ͤ���ּ�";   //��̬������Ʒ��ʾ��Ϣ
                    camcon.Hint_Interact.enabled = true;
                    return;
                }
                else if (item.itemNumber == 2)
                {
                    InventoryManager.instance.playerBag.itemList.RemoveAt(i);
                    False_Lanting.AddNewItem();

                    camcon.Hint_Interact.text = "�����ͤ����ġ��";   //��̬������Ʒ��ʾ��Ϣ
                    camcon.Hint_Interact.enabled = true;

                    camcon.TaskList.ElapseTime = 0;        //�����ʱ����
                    camcon.TaskList.Count++;       //��ʾ��һ��������ʾ

                    camcon.BagSystem.SetActive(false);      //���ر���

                    BlurImage_Bag.SetActive(false);     //�����黯�ر�
                    camcon.isOpen = false;          //��ֹ�򿪱���������

                    KBI.mouseEnable = true;        //��������ͷ�ƶ�
                    KBI.KeyEnable = true;          //���ü�λ����



                    Cursor.lockState = CursorLockMode.Confined;     //���������ʾ
                    Cursor.visible = true; ;
                    return;
                }
                else
                {
                    camcon.Hint_Interact.text = "����������";   //��̬������Ʒ��ʾ��Ϣ
                    camcon.Hint_Interact.enabled = true;
                }
            }
        }
        InventoryManager.RefreshItem();     //ˢ�±���
    }
    /// <summary>
    /// ��Ʒ�����¼�
    /// </summary>
    public void ItemDiscard()
    {
        for (int i = 0; i < InventoryManager.instance.playerBag.itemList.Count; i++)
        {
            if (SlotNumber == InventoryManager.instance.playerBag.itemList[i].Number)
            {
                InventoryManager.instance.playerBag.itemList.RemoveAt(i);
            }
        }
        InventoryManager.RefreshItem();     //ˢ�±���
    }
}
