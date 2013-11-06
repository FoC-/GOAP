using System;
using System.Collections.Generic;

namespace Core.Planning
{
    public interface IPlaningState : ICloneable, IEquatable<IPlaningState>
    {
        string CreatedBy { get; set; }
        IEnumerable<Parameter> GetAll();
        Parameter Get(string id);
        void Save(Parameter parameter);
        void Remove(string id);
        double Distance(IPlaningState destination);
    }
}