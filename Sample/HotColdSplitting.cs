using UnityEngine;
using System.Collections.Generic;

namespace ECSII.Test
{
    //���ȷָ���ԣ�
    public class HotColdSplitting : MonoBehaviour
    {
        public enum Mode
        {
            OOP, DOD, OOPwithSplit
        }

        [SerializeField] private Mode mode;
        private long count = 1000000;

        private List<PlayerOOP> playerOOPs = new List<PlayerOOP>();
        private List<PlayerDOD> playerDODs = new List<PlayerDOD>();
        private List<PlayerOOPWithSplitting> withSplitting = new List<PlayerOOPWithSplitting>();

        private void Start()
        {
            if (mode == Mode.DOD)
            {
                for (int i = 0; i < count; i++)
                {
                    PlayerDOD p = new PlayerDOD();
                    playerDODs.Add(p);
                }
            }
            else if (mode == Mode.OOPwithSplit)
            {
                for (int i = 0; i < count; i++)
                {
                    PlayerOOPWithSplitting p = new PlayerOOPWithSplitting();
                    withSplitting.Add(p);
                }
            }
            else if (mode == Mode.OOP)
            {
                for (int i = 0; i < count; i++)
                {
                    PlayerOOP p = new PlayerOOP();
                    playerOOPs.Add(p);
                }
            }

            Debug.Log("׼�����");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (mode == Mode.DOD)
                {
                    float startTime = Time.realtimeSinceStartup;
                    for (int i = 0; i < count; i++)
                    {
                        playerDODs[i].LevelUp();
                    }
                    Debug.Log($"DOD:{(Time.realtimeSinceStartup - startTime) * 1000} ms"); //10ms
                }
                else if (mode == Mode.OOPwithSplit)
                {
                    float startTime = Time.realtimeSinceStartup;
                    for (int i = 0; i < count; i++)
                    {
                        withSplitting[i].LevelUp();
                    }
                    Debug.Log($"OOPwithSplit:{(Time.realtimeSinceStartup - startTime) * 1000} ms"); //10ms
                }
                else if (mode == Mode.OOP)
                {
                    float startTime = Time.realtimeSinceStartup;
                    for (int i = 0; i < count; i++)
                    {
                        playerOOPs[i].LevelUp();
                    }
                    Debug.Log($"OOP:{(Time.realtimeSinceStartup - startTime) * 1000} ms"); //21ms
                }
            }
        }
    }

    //�������ȷָ�
    public class PlayerOOPWithSplitting
    {
        public int Level;
        public PlayerOOP PlayerOOP;

        public void LevelUp()
        {
            Level++;
        }
    }

    //�������е���Ϣ������ÿ���������ø����ˣ��������ܹ����ص� cache line �е����������ÿ֡��ÿ��������Ὣ�������ݼ��ص��ڴ���ȥ����ʹ���Ǹ�������ȥʹ������
    public class PlayerOOP
    {
        public long Health = 1000000000000;
        public long Pos = 1000000000000;
        public long Scale = 1000000000000;
        public long Rot = 1000000000000;
        public long Attack = 1000000000000;
        public long Health1 = 1000000000000;
        public long Pos1 = 1000000000000;
        public long Scale1 = 1000000000000;
        public long Rot1 = 1000000000000;
        public string Attack1 = "1000000000000";
        public string Health2 = "1000000000000";
        public string Pos2 = "1000000000000";
        public string Scale2 = "1000000000000";
        public string Rot2 = "1000000000000";
        public string Attack2 = "1000000000000";
        public string Health3 = "1000000000000";
        public string Pos3 = "1000000000000";
        public string Scale3 = "1000000000000";
        public string Rot3 = "1000000000000";
        public string Attack3 = "1000000000000";
        public string[] Invetory = new string[10];
        public long Level;

        public void LevelUp()
        {
            Level++;
        }
    }

    //DOD��ʽ������
    public class PlayerDOD
    {
        public int Level;

        public void LevelUp()
        {
            Level++;
        }
    }
}