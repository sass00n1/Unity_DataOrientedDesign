using UnityEngine;

namespace ECSII.Test
{
    //SoA 与 AoS 示例：
    public class SoA : MonoBehaviour
    {
        const int COUNT = 1024 * 100;
        enum Mode { SoA, AoS }

        [SerializeField] private Mode mode;

        private SoA_strcut simd_Struct;
        private AoS_struct[] misd_Strcut;

        private void Start()
        {
            if (mode == Mode.SoA)
            {
                simd_Struct = new SoA_strcut();
            }

            if (mode == Mode.AoS)
            {
                misd_Strcut = new AoS_struct[COUNT];
                for (int i = 0; i < COUNT; i++)
                {
                    AoS_struct strcut = new AoS_struct();
                    misd_Strcut[i] = strcut;
                }
            }
            Debug.Log("数据准备完毕");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //SoA的好处：
                //1.充分利用了Cache Line
                //2.数据是连续的，很好的进行硬件预取
                //3.可以SIMD
                if (mode == Mode.SoA)
                {
                    float startTime = Time.realtimeSinceStartup;

                    long value = 0;
                    for (int i = 0; i < COUNT; i++)
                    {
                        value += simd_Struct.pos[i];
                    }

                    Debug.Log(value);
                    Debug.Log($"SoA(高性能):{(Time.realtimeSinceStartup - startTime) * 1000} ms");
                }

                if (mode == Mode.AoS)
                {
                    float startTime = Time.realtimeSinceStartup;

                    long value = 0;
                    for (int i = 0; i < COUNT; i++)
                    {
                        value += misd_Strcut[i].pos;
                    }

                    Debug.Log(value);
                    Debug.Log($"AoS(低性能):{(Time.realtimeSinceStartup - startTime) * 1000} ms");
                }
            }
        }

        public class SoA_strcut
        {
            public long[] pos = new long[COUNT];
            public long[] health = new long[COUNT];
            public long[] attack = new long[COUNT];
            public long[] pos1 = new long[COUNT];
            public long[] health1 = new long[COUNT];
            public long[] attack1 = new long[COUNT];
            public long[] pos2 = new long[COUNT];
            public long[] health2 = new long[COUNT];
            public long[] attack2 = new long[COUNT];

            public SoA_strcut()
            {
                for (int i = 0; i < COUNT; i++)
                {
                    pos[i] = 1234;
                    health[i] = 1234;
                    attack[i] = 1234;
                    pos1[i] = 1234;
                    health1[i] = 1234;
                    attack1[i] = 1234;
                    pos2[i] = 1234;
                    health2[i] = 1234;
                    attack2[i] = 1234;
                }
            }
        }

        public class AoS_struct
        {
            public long pos;
            public long health;
            public long attack;
            public long pos1;
            public long health1;
            public long attack1;
            public long pos2;
            public long health2;
            public long attack2;

            public AoS_struct()
            {
                pos = 1234;
                health = 1234;
                attack = 1234;
                pos1 = 1234;
                health1 = 1234;
                attack1 = 1234;
                pos2 = 1234;
                health2 = 1234;
                attack2 = 1234;
            }
        }
    }
}