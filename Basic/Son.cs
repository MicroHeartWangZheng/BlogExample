using System;
using System.Collections.Generic;
using System.Text;

namespace Basic
{
    public class Son : Parent
    {
        public override string Name { get; set; } = "1";


        public Son() : base()
        {
            Console.WriteLine("Son:" + Name);
        }
        public Son(string name) : base(name)
        {
            this.Name = name;
        }
    }


    public class Parent
    {

        public virtual string Name { get; set; } = "2";

        public Parent()
        {
            Console.WriteLine("Parent:" + Name);
        }

        public Parent(string name)
        {
            this.Name = name;
        }
    }
}
