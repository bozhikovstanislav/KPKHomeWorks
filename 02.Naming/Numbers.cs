namespace _02.Naming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Number
    {
        const int MAX_COUNT = 6;

        public class MaxNumberChecker
        {
           public void CheckBoleanState(bool isBigerThenMaxCount)
            {
                string boolValue = isBigerThenMaxCount.ToString();
                Console.WriteLine(boolValue);
            }
        }
        public static void isCountBigerthenMAxCount()
        {
            Number.MaxNumberChecker countNumber =
              new Number.MaxNumberChecker();
            countNumber.CheckBoleanState(true);
        }
    }
}

