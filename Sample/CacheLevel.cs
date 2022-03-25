using UnityEngine;

namespace ECSII.Test
{
    //缓存级别示例：
    //提高数据的缓存级别到寄存器
    //甚至数据的迭代顺序会对性能造成影响
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

            Debug.Log("数据准备完毕");
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
                    Debug.Log($"缓存级别高:{(Time.realtimeSinceStartup - startTime) * 1000} ms");
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
                    Debug.Log($"缓存级别低:{(Time.realtimeSinceStartup - startTime) * 1000} ms");
                    //缓存级别分为：CPU寄存器 > L1 > L2/L3 > 主内存 > 硬盘
                    //数据加载到寄存器中，按正确顺序迭代，都是在寄存器中寻找到，速度非常快。
                    //此处没有按正确顺序迭代，导致CPU寄存器中找不到数据，于是到了L1及L2等后续内存中寻找，缓存未命中造成CPU空转。
                }
            }
        }
    }
}