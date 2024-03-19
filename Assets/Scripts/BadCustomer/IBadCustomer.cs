using System.Collections.Generic;
using UnityEngine;
using Interfaces;

namespace BadCustomer
{
    public interface IBadCustomer
    {
        public List<IInteractable> Path { get; }

        public void SetDestination(Vector3 position);

        public bool IsAtTargetPoint();

        public void Reset();
    }
}