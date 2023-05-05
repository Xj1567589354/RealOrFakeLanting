using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.IP
{
    public class PlayerInertia : MonoBehaviour
    {
        private IUserInput Pi;
        private ActorController Act;


        void Awake()
        {
            Pi = GetComponent<IUserInput>();
            Act = GetComponent<ActorController>();
        }
        public void Inertia()
        {
            /*
     ������������
     ����ģ���ƶ������ԣ�
     */
            Pi.InputEnabled = false;
            Act.lockplaner = true;
        }
    }
}

