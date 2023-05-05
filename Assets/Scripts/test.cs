using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tes.IP
{
    public class test : MonoBehaviour
    {
        [Range(-2.0f, 5.0f)]     //限定属性值范围
        public float speed = 2.0f;
        [HideInInspector]       //隐藏属性
        public Direction MoveDir;
        public int[,] array_one;        //二维数组
        public int[][] array_two;       //交叉数组


        private void Start()
        {
            array_one = new int[3, 4];
            array_two = new int[][] { new int[] { 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8, 9 }, new int[] { 10, 20, 30, 34, 55 } };
        }

        private void Update()
        {
          
        }


        /// <summary>
        /// 枚举类
        /// </summary>
        public enum Direction
        {
            Left,Right,Up,Down
        }
        /// <summary>
        /// 4*4二维数组赋值函数
        /// </summary>
        /// <param name="array_row">行</param>
        /// <param name="array_column">列</param>
        /// <returns>数组元素</returns>
        public int Array_assign(int array_row,int array_column)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    array_one[i, j] = i + j;
                }
            }
            return(array_one[array_row, array_column]);
        }
    }
}

