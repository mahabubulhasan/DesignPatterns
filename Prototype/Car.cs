using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype
{
    public interface IDeepCopyable<T> { T DeepCopy(); }

    public class Car: IDeepCopyable<Car>
    {
        public string Brand { get; set; }
        public Model Model { get; set; }

        public Car DeepCopy()
        {
            Car clone = (Car)this.MemberwiseClone();
            clone.Model = clone.Model.DeepCopy();
            return clone;
        }
    }

    public class Model: IDeepCopyable<Model>
    {
        public string Year { get; set; }

        public Model DeepCopy()
        {
            Model clone = (Model)this.MemberwiseClone();
            return clone;
        }
    }
}
