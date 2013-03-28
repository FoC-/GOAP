using System;
using System.Collections.Generic;
using Core.PlaningActions;

namespace Core.States
{
    public interface IState<T> : ICloneable, IEquatable<IState<T>>
    {
        void Add(Dictionary<T, int> items);
        void Remove(Dictionary<T, int> items);
        bool Has(T item, int count);
        int Count(T item);
        int ParametersCount();
        double Distance(IState<T> destination);
        void CreatedBy(PlanningAction<T> planningAction);
    }
}