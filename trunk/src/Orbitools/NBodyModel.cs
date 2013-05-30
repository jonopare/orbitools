using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orbitools
{
    public class NBodyModel : ICollection<Body>
    {
        private readonly List<Body> bodies;

        public int IterativeRateMultiplier { get; set; }

        public EventHandler<BodyEventArgs> BodyRemoved;

        public NBodyModel(int iteratativeRateMultiplier = 1)
        {
            bodies = new List<Body>();
            IterativeRateMultiplier = iteratativeRateMultiplier;
        }

        /// <summary>
        /// The update method works iteratively. 
        /// </summary>
        /// <param name="sinceLastUpdate">Simulation time since last update.</param>
        public void Update(TimeSpan sinceLastUpdate)
        {
            for (int k = 0; k < IterativeRateMultiplier; k++)
            {
                var forces = Body.Gravity(bodies);
                for (int i = 0; i < forces.Count; i++)
                {
                    bodies[i].ApplyForce(forces[i], sinceLastUpdate);
                }

                RemoveInvalidated();
            }
        }

        private void RemoveInvalidated()
        {
            var outOfBounds = bodies.Where(b => b.IsInvalid).ToArray();
            if (outOfBounds.Length > 0)
            {
                foreach (var body in outOfBounds)
                {
                    bodies.Remove(body);
                    OnBodyRemoved(body);
                }
            }
        }

        protected void OnBodyRemoved(Body body)
        {
            var bodyRemoved = BodyRemoved;
            if (bodyRemoved != null)
            {
                bodyRemoved(this, new BodyEventArgs(body));
            }
        }

        #region ICollection<Body>
        public void Add(Body item)
        {
            if (item.IsInvalid)
            {
                throw new InvalidOperationException();
            }
            bodies.Add(item);
        }

        public void Clear()
        {
            bodies.Clear();
        }

        public bool Contains(Body item)
        {
            return bodies.Contains(item);
        }

        public void CopyTo(Body[] array, int arrayIndex)
        {
            bodies.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return bodies.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Body item)
        {
            return bodies.Remove(item);
        }

        public IEnumerator<Body> GetEnumerator()
        {
            return bodies.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return bodies.GetEnumerator();
        }
        #endregion

        
    }
}
