using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using NUnit.Framework;

namespace Stairs
{
    [TestFixture]
    class Test
    {
        //Этот тест взял с тимуса 
        [TestCase(212, "995645335")]
        [TestCase(3, "1")]
        [TestCase(5,"2")]

        public void TestCases(int count, string expectedStr)
        {
            var expected = BigInteger.Parse(expectedStr);
            Assert.AreEqual(Program.GetStairsCount(count), expected);
        }
    }

    class Program
    {
        public static BigInteger GetStairsCount(int cubeCount)
        {
            //В массиве opt[i1,i2] лежит число лестниц из кубиков i1 высоты не больeе i2
            //Так как высота ступеней может только возрастать, то количество лестниц высоты i2 из i1 кубиков
            //Равно количесву лестниц без столбца i2(самый высокий), то есть лестницы из i1-i2 кубиков 
            //По всем высотам от минимальной 1 до i2-1

            ///По другому работу алгоритма можно описать так:
            /// при добаления нового кубика максимальная высота i-1
            /// новый кубик мы пытаемся поставить на позиции разной высоты, начиная с минимальной 2 (за это отвечает второй цикл)
            /// и фиксируем этот столбец, так как в данный момент времени эта позиция считается максимальной высотой 
            /// а дальше просто рассматриваем лесницы из оставшихся кубиков с высотами строго меньше высоты фиксированного столбца
            var opt = new BigInteger[cubeCount + 1, cubeCount + 1];
            opt[1, 1] = 1;
            opt[2, 2] = 1;
            for (int i = 3; i <= cubeCount; i++)
            {
                for (int j = 2; j <= i-1; j++)
                    for (int a = 1; a < j; a++)
                        opt[i, j] += opt[i - j, a];
                opt[i, i] = 1;
            }
            var result = BigInteger.MinusOne;
            for (int i = 0; i <= cubeCount; ++i)
                result += opt[cubeCount, i];
            return result;
        }
        static void Main(string[] args)
        {
        }
    }
}
