using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    public Text Task_Context;
    public Text Task_Title;
    public Image Sign;
    public float Count;     //场景序号
    public float ElapseTime;        //实时时间
    public float FadeTime = 0;
    public CameraController camcon;

    private void Start()
    {
        Count = 0;
        ElapseTime = 0;
    }

    private void Update()
    {
        print(Count);
        switch (Count)
        {
            case 1:
                Task_Title.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //任务标题显示
                Sign.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);     //任务标题提示图片
                Task_Context.text = "前往裴府书房二楼与叔父裴度对话";
                Task_Context.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);       //任务提示更新
                break;
            case 2:
                Task_Update("前往武府了解实情");
                break;
            case 3:
                Task_Update("前往听侯亭寻找目击者");
                break;
            case 4:
                Task_Update("调查武元衡尸体");
                break;
            case 5:
                Task_Update("前往酒馆");
                break;
            case 6:
                Task_Update("与商贩对话");
                break;
            case 7:
                Task_Update("前往绿竹林捕杀野兽换取银两购买商贩碎片");
                break;
            case 8:
                Task_Update("前往李贺书院");
                break;
            case 9:
                Task_Update("与生员对话");
                break;
            case 10:
                Task_Update("与二嘎子对话");
                break;
            case 11:
                Task_Update("前往大理寺赴白头之约");
                break;
            case 12:
                Task_Update("前往禅房");
                break;
            case 13:
                Task_Update("前往禅房二楼与僧人辩才对话");
                break;
            case 14:
                Task_Update("与阿尘对话");
                break;
            case 15:
                Task_Update("在大理寺内寻找李贺");
                break;
            case 16:
                Task_Update("调查李贺尸体");
                break;
            case 17:
                Task_Update("返回书院与韩湘子生员对话");
                break;
            case 20:
                Task_Update("与韩湘子对话");
                break;
            case 21:
                Task_Update("返回绿竹林捕鱼与韩湘子换取碎片");
                break;
            case 22:
                Task_Update("前往崔府");
                break;
            case 23:
                Task_Update("与崔淼对话");
                break;
            case 24:
                Task_Update("前往绿竹林北岸捕杀野生熊与崔淼换取碎片");
                break;
            case 25:
                Task_Update("前往贾府");
                break;
            case 26:
                Task_Update("与贾昌对话");
                break;
            case 27:
                Task_Update("寻找贾昌的碎片");
                break;
            case 28:
                Task_Update("将《兰亭序》碎片合成一副完整的《兰亭序》");            //合成功能
                break;
            case 29:
                Task_Update("查看《兰亭序》临摹版");
                break;
            case 30:
                Task_Update("前往萧府与萧翼对话");
                break;
            case 31:
                Task_Update("前往大明宫甘露殿了解实情");
                break;
            case 32:
                Task_Update("前往甘露殿二楼与唐太宗对话");
                break;
            case 33:
                Task_Update("在大明宫内寻找真迹《兰亭序》");
                break;
            case 34:
                Task_Update("返回大理寺了结辩才心愿");
                break;
            case 35:
                Task_Title.text = "（完成） 寻找《兰亭序》";
                FadeTime += Time.deltaTime;
                if (FadeTime > 3.0f)        //3s后任务提示消失
                {
                    Task_Title.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                    Sign.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                    Task_Context.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);
                    FadeTime = 0;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 任务更新事件
    /// </summary>
    /// <param name="_Word">任务内容</param>
    public void Task_Update(string _Word)
    {
        Task_Context.gameObject.SendMessage("UIHide", SendMessageOptions.DontRequireReceiver);      //上一次任务提示消失
        ElapseTime += Time.deltaTime;
        if (ElapseTime >= 2.0f)         //2s后任务提示更新
        {
            Task_Context.text = _Word;
            Task_Context.gameObject.SendMessage("UIShow", SendMessageOptions.DontRequireReceiver);
        }
    }
}
