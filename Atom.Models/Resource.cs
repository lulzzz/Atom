using System;
using System.Collections;
using System.Collections.Generic;

namespace Atom.Models
{
    public class Resource : IComparable<Resource>
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public ResourceType[] Types { get; set; }
        public string[] Tags { get; set; }
        public Resource()
        {

        }
        public Resource(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
      

        public int CompareTo(Resource other)
        {
            if(this.Name != other.Name)
            {
                throw new Exception("Ресурсы не сравнимы");
            }
            if(this.Value > other.Value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
           
        }


    }
    public class NameEqualityComparer : IEqualityComparer<Resource>
    {
        public bool Equals(Resource x, Resource y)
        {
            if (y == null && x == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Name == y.Name)
                return true;
            else
                return false;
        }

        public int GetHashCode(Resource obj)
        {
            string hCode = obj.Name;
            return hCode.GetHashCode();
        }
    }
}
