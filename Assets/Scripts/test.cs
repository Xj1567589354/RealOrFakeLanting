using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tes.IP
{
    public class test : MonoBehaviour
    {
        [Range(-2.0f, 5.0f)]     //�޶�����ֵ��Χ
        public float speed = 2.0f;
        [HideInInspector]       //��������
        public Direction MoveDir;
        public int[,] array_one;        //��ά����
        public int[][] array_two;       //��������


        private void Start()
        {
            array_one = new int[3, 4];
            array_two = new int[][] { new int[] { 1, 2 }, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8, 9 }, new int[] { 10, 20, 30, 34, 55 } };
        }

        private void Update()
        {
          
        }


        /// <summary>
        /// ö����
        /// </summary>
        public enum Direction
        {
            Left,Right,Up,Down
        }
        /// <summary>
        /// 4*4��ά���鸳ֵ����
        /// </summary>
        /// <param name="array_row">��</param>
        /// <param name="array_column">��</param>
        /// <returns>����Ԫ��</returns>
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

