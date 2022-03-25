using UnityEngine;

namespace ECSII.Test
{
    //���漶��ʾ����
    //������ݵĻ��漶�𵽼Ĵ���
    //�������ݵĵ���˳�����������Ӱ��
    public class CacheLevel : MonoBehaviour
    {
        enum CacheLevelMode { HIGHT, LOW }

        [SerializeField] private CacheLevelMode cacheLevelMode;
        private long[,] values = new long[2048, 2048];

        private void Start()
        {
            for (int y = 0; y < 2048; y++)
            {
                for (int x = 0; x < 2048; x++)
                {
                    values[y, x] = Random.Range(0,99999999);
                }
            }

            Debug.Log("����׼�����");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (cacheLevelMode == CacheLevelMode.HIGHT)
                {
                    float startTime = Time.realtimeSinceStartup;
                    long num = 0;
                    for (int y = 0; y < 2048; y++)
                    {
                        for (int x = 0; x < 2048; x++)
                        {
                            num += values[y, x];
                        }
                    }
                    Debug.Log(num);
                    Debug.Log($"���漶���:{(Time.realtimeSinceStartup - startTime) * 1000} ms");
                }

                if (cacheLevelMode == CacheLevelMode.LOW)
                {
                    float startTime = Time.realtimeSinceStartup;
                    long num = 0;
                    for (int x = 0; x < 2048; x++)
                    {
                        for (int y = 0; y < 2048; y++)
                        {
                            num += values[y, x];
                        }
                    }
                    Debug.Log(num);
                    Debug.Log($"���漶���:{(Time.realtimeSinceStartup - startTime) * 1000} ms");
                    //���漶���Ϊ��CPU�Ĵ��� > L1 > L2/L3 > ���ڴ� > Ӳ��
                    //���ݼ��ص��Ĵ����У�����ȷ˳������������ڼĴ�����Ѱ�ҵ����ٶȷǳ��졣
                    //�˴�û�а���ȷ˳�����������CPU�Ĵ������Ҳ������ݣ����ǵ���L1��L2�Ⱥ����ڴ���Ѱ�ң�����δ�������CPU��ת��
                }
            }
        }
    }
}