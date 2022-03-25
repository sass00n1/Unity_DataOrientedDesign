using UnityEngine;
using System.Collections.Generic;

namespace ECSII.Test
{
    //ָ����ˮ��֮��֧Ԥ�� ���ԣ�
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

            Debug.Log("׼�����");
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
                        //DOD��ʽ����OOPһ����Ҳ�ڴ˴���������ȫ��ͬ��if��֧�������������ݷǳ�ƽ̹��CPUû��Ԥ��ʧ��Ӱ�����ܵ����������
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
                        //ָ����ˮ����ͣ��
                        //CPU��ָ����ˮ�߷�Ϊ������׶Σ�1.Fetch 2.Decode 3.Execute 4.Access 5.WriteBack��
                        //������·���У�����֮ǰ�Ĵ���ѡ����������֧Ȼ�����������ǵ�ѭ�������ڻ�Ծ�ĺͲ���Ծ������֮��ת����Ԥ���ʧ���ˡ�
                        //������ˮ����ͣ��
                        if (particleOOP[i].IsActive())
                        {
                            particleOOP[i].Update();
                        }
                    }

                    Debug.Log($"OOP:{(Time.realtimeSinceStartup - startTime) * 1000} ms"); //170ms
                }

                //���ڴ�������Ż��ֶξ��ǣ����һ��ƽ�ȵ����ݣ��������CPU�����CPUԤ��ʧ�ܡ�
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