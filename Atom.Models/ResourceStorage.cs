using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Atom.Models
{
    public class ResourceStorage : ICollection<Resource>
    {
        List<Resource> resources { get; set; } = new List<Resource>();
        List<Flow> Flows { get; set; } = new List<Flow>();
        public int Count => resources.Count;

        public bool IsReadOnly => false;

        public void Add(Resource item)
        {
            resources.Add(item);
        }

        public void Add(IEnumerable<Resource> items)
        {
            resources.AddRange(items);
        }

        public ResourceStorage Sum()
        {
            ResourceStorage result = new ResourceStorage();
            var distincts = resources.Distinct(new NameEqualityComparer()).ToList();
            distincts.ForEach(x =>
            {
                var newResource = resources.Where(c => c.Name == x.Name).ToArray();
                dynamic sum = 0.0d;
                for (var i = 0; i < newResource.Length; i++)
                {
                    sum += newResource[i].Value;
                }
                result.Add(new Resource(x.Name, sum));
            });
            return result;
        }

        public ResourceStorage GetResources(DateTime dateTime)
        {
            ResourceStorage buf = Sum();            
            Flows.ForEach(x =>
            {
                var val = x.GetValue(date: dateTime);
                buf.resources.Add(new Resource(x.ResourceName, val));
            });
            ResourceStorage result = buf.Sum();            
            return result;
        }

        public static ResourceStorage operator -(ResourceStorage r1, ResourceStorage r2)
        {   
            r1.Add(-r2);
            return r1.Sum();
        }
        public static ResourceStorage operator -(ResourceStorage r)
        {
            r.resources.ForEach(x =>
            {
                x.Value = -x.Value;
            });          
            return r;
        }


        public void Clear()
        {
            resources = new List<Resource>();
        }

        public bool Contains(Resource item)
        {
            return resources.Contains(item);
        }

        public void CopyTo(Resource[] array, int arrayIndex)
        {
            resources.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return resources.GetEnumerator();
        }

        public bool Remove(Resource item)
        {
            return resources.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return resources.GetEnumerator();
        }
    }
}
