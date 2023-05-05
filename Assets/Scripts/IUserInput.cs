using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Space(5)]
    [Header("=====  Output Signals  =====")]
    public float dup;
    public float dright;
    public float dup2;      //定义新的球形坐标轴 UV
    public float dright2;
    public float Jup;
    public float Jright;
    public float dri;

    public bool Run;
    public bool Jump;
    public bool Defense;
    public bool Roll;
    public bool Lockon;         // 锁定
    public bool Interact;         // 交互
    public bool Quit_Dialogue;         // 退出对话

    protected bool lastjump;
    protected bool newjump;
    public bool Attack;
    protected bool lastAttack;
    protected bool newAttack;
    /*
 * 向量模
 */
    public float dmap;
    public Vector3 dvec;
    protected Vector2 tempdAxis;


    [Space(5)]
    [Header("=====  Others =====")]
    public bool InputEnabled = true;
    public float targetdright;
    public float targetdup;
    protected float velocityDup;
    protected float velocityDright;
    /// <summary>
    /// 球形坐标轴转换函数
    /// </summary>
    /// <param name="input">键值</param>
    /// <returns>坐标转换结果</returns>
    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
}
