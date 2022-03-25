# Unity_DataOrientedDesign
关于Unity的面向数据编程的性能测试示例，用于了解如何有效利用CPU缓存，以及ECS架构的背后意图与动机。
# 面向数据的动机
当今CPU的处理速度正在相对放缓。我们在过去20年中所看到的增长率已不太可能重现。对于许多硬件系统而言，内存访问速度并未随着处理速度的提高而提高。当今电子游戏的性能瓶颈需要以极高的速度访问内存。需要一种排列和访问内存数据的方法，从而可以有效地利用这种速度。
![图片](https://user-images.githubusercontent.com/41114110/160059684-6893bb99-140a-40ae-9c6c-ee2bac51527d.png)
# 包含的示例
### 1.缓存等级
此示例演示了如何有效利用CPU较高等级的缓存。以及错误的迭代顺序导致不能在寄存器中找到高速缓存，而延迟到L1乃至L2L3缓存造成CPU空转影响性能的情况。

![图片](https://user-images.githubusercontent.com/41114110/160047752-4cac9c4a-d41d-454e-aa8a-9b38d73484c7.png)
### 2.指令流水线
此示例演示了如何安排CPU指令流水线。错误的数据编排会造成CPU分支预测失败，导致指令流水线暂停，以至于性能下降。此示例最贴切的实际例子就是粒子系统的优化方式。

![图片](https://user-images.githubusercontent.com/41114110/160055237-616c53f3-a043-49e4-aa39-9fe557dc4692.png)
### 3.SoA vs AoS
此示例演示了SoA(数组型结构)在CPU性能上的优势。单个SIMD寄存器可以加载同构数据，可能通过内部数据路径(例如128位)传输。如果只需要记录的特定部分，则只需要遍历这些部分，从而允许更多的数据装入单个Cache Line。

![图片](https://user-images.githubusercontent.com/41114110/160056157-16690196-f298-4da9-9c37-351c19aa7f09.png)
### 4.冷热分割
此示例演示了如何构造冷数据与热数据。为了避免无用的冷数据占用CPU的高速缓存，为数据进行冷热分割，有效利用缓存，减少缓存未命中。

![图片](https://user-images.githubusercontent.com/41114110/160058393-f2101d0f-ac6f-401c-bc2a-70695ab13b6c.png)

# 相关引用
* CppCon 2014: Mike Acton "Data-Oriented Design and C++" https://www.youtube.com/watch?v=rX0ItVEVjHc&list=PL8X2-uFjx4px_emmb2fRcBqFNS6fuvaHQ&index=24&t=1840s
* Data Locality http://gameprogrammingpatterns.com/data-locality.html
* Data-Oriented Design https://www.dataorienteddesign.com/dodmain/
* Data-Oriented Design (Or Why You Might Be Shooting Yourself in The Foot With OOP) https://gamesfromwithin.com/data-oriented-design
