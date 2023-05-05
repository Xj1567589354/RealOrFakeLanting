using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Space(5)]
    [Header("=====  Output Signals  =====")]
    public float dup;
    public float dright;
    public float dup2;      //�����µ����������� UV
    public float dright2;
    public float Jup;
    public float Jright;
    public float dri;

    public bool Run;
    public bool Jump;
    public bool Defense;
    public bool Roll;
    public bool Lockon;         // ����
    public bool Interact;         // ����
    public bool Quit_Dialogue;         // �˳��Ի�

    protected bool lastjump;
    protected bool newjump;
    public bool Attack;
    protected bool lastAttack;
    protected bool newAttack;
    /*
 * ����ģ
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
    /// ����������ת������
    /// </summary>
    /// <param name="input">��ֵ</param>
    /// <returns>����ת�����</returns>
    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
}
