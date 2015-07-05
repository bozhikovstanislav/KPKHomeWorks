

namespace _02.Naming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Hauptklasse
    {
        enum Gender { male, fmale };
        class Man
        {
            public Gender Gender { get; set; }
            public string ManName { get; set; }
            public int Age { get; set; }
        }
        public void Make_Чуек(int numberOfTheMan)
        {
            Man manInstance = new Man();
            manInstance.Age = numberOfTheMan;
            if (numberOfTheMan % 2 == 0)
            {
                manInstance.ManName = "Батката";
                manInstance.Gender = Gender.male;
            }
            else
            {
                manInstance.ManName = "Мацето";
                manInstance.Gender = Gender.fmale;
            }
        }
    }
}
