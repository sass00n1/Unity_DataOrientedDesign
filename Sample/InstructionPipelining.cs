using UnityEngine;
using System.Collections.Generic;

namespace ECSII.Test
{
    //指令流水线之分支预测 测试：
    public class InstructionPipelining : MonoBehaviour
    {
        [SerializeField] private bool isDOD;

        private long count = 10000000;
        private List<ParticleOOP> particleOOP = new List<ParticleOOP>();
        private List<ParticleDOD> particleDOD = new List<ParticleDOD>();

        private void Start()
        {
            if (isDOD)
            {
                for (int i = 0; i < count; i++)
                {
                    ParticleDOD p = new ParticleDOD();
                    particleDOD.Add(p);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    ParticleOOP p = new ParticleOOP();
                    p.isActive = Random.Range(0, 3) == 0 ? true : false;
                    particleOOP.Add(p);
                }
            }

            Debug.Log("准备完毕");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isDOD)
                {
                    float startTime = Time.realtimeSinceStartup;

                    for (int i = 0; i < count; i++)
                    {
                        //DOD方式：和OOP一样，也在此处进行了完全相同的if分支。由于整段数据非常平坦，CPU没有预测失败影响性能的情况发生。
                        if (particleDOD[i].IsActive())
                        {
                            particleDOD[i].Update();
                        }
                    }

                    Debug.Log($"DOD:{(Time.realtimeSinceStartup - startTime) * 1000} ms"); //104ms
                }
                else
                {
                    float startTime = Time.realtimeSinceStartup;

                    for (int i = 0; i < count; i++)
                    {
                        //指令流水线暂停：
                        //CPU的指令流水线分为五个个阶段：1.Fetch 2.Decode 3.Execute 4.Access 5.WriteBack。
                        //多条电路并行，它看之前的代码选择了哪条分支然后照做。但是当循环不断在活跃的和不活跃的粒子之间转换，预测就失败了。
                        //导致流水线暂停。
                        if (particleOOP[i].IsActive())
                        {
                            particleOOP[i].Update();
                        }
                    }

                    Debug.Log($"OOP:{(Time.realtimeSinceStartup - startTime) * 1000} ms"); //170ms
                }

                //对于此问题的优化手段就是：打包一组平稳的数据，避免颠簸CPU缓存和CPU预测失败。
            }
        }
    }

    public class ParticleDOD
    {
        public bool isActive;

        public bool IsActive()
        {
            return isActive;
        }

        public void Update()
        {

        }
    }

    public class ParticleOOP
    {
        public bool isActive;

        public bool IsActive()
        {
            return isActive;
        }

        public void Update()
        {

        }
    }
}