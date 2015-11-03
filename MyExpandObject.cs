using System;
using System.Collections.Generic;
using System.Dynamic;

namespace ConsoleApplication1
{
    public class MyExpandObject : DynamicObject
    {
        private Dictionary<string, object> _dict =
            new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (_dict.ContainsKey(binder.Name.ToUpper()))
            {
                result = _dict[binder.Name.ToUpper()];
                return true;
            }
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (_dict.ContainsKey(binder.Name.ToUpper()))
                _dict[binder.Name.ToUpper()] = value;
            else
                _dict.Add(binder.Name.ToUpper(), value);
            return true;
        }
    }

    class TestMyExandoObject
    {
        static void Main(string[] args)
        {
            dynamic vessel = new MyExpandObject();
            vessel.Name = "Little Miss Understood";
            vessel.Age = 12;
            vessel.KeelLengthInFeet = 32;
            vessel.Longitude = 37.55f;

            Console.WriteLine("the {0} year old vessel" +
                "named {1} has a keel length of {2} feet",
                vessel.AGE, vessel.nAme, vessel.keelLengthInfeet);

            Console.ReadKey();
        }
    }
}
