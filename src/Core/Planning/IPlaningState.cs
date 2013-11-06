using System;

namespace Core.Planning
{
    public interface IPlaningState : ICloneable
    {
        string CreatedBy { get; set; }
    }
}