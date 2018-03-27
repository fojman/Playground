using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PlaygroundGeneric.Tests
{
    public class Class1
    {
        [Test]
        public void TestAndDebugMyCode()
        {
            var t = new Hashtable();

            t.Add("1", "one");
            //t.Add("1", "one_x");

            t["1"] = "new_one";
        }
    }
}
