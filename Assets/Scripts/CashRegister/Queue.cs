using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

[Serializable]
public class Queue
{
    [field:SerializeField] public Vector3 ShiftForNextPosition { get; private set; }

    private Transform _firstPositionInQueue;
    private Vector3 _lastBusyPositionInQueue;
    private readonly List<ICustomer> _customersInLine = new();

    public void Initialize(Transform pointForCustomers)
    {
        _firstPositionInQueue = pointForCustomers;
        _lastBusyPositionInQueue = _firstPositionInQueue.position;
    }
    public Vector3 GetFreePosition(ICustomer customer)
    {
        _customersInLine.Add(customer);
        
        if (_firstPositionInQueue.position == _lastBusyPositionInQueue)
        {
            _lastBusyPositionInQueue += ShiftForNextPosition;
            return _firstPositionInQueue.position;
        }
        return _lastBusyPositionInQueue += ShiftForNextPosition;
    }
    public void OnCustomerLeft(ICustomer customer)
    {
        _customersInLine.Remove(customer);
        if (_lastBusyPositionInQueue == _firstPositionInQueue.position)
        {
            return;
        }
        Debug.Log("customerLeft");
        Debug.Log("LastBusyPositionInLine before " + _lastBusyPositionInQueue);
        _lastBusyPositionInQueue -= ShiftForNextPosition;
        Debug.Log("LastBusyPositionInLine after " + _lastBusyPositionInQueue);
        MoveCustomersForward();
    }

    private void MoveCustomersForward()
    {
        foreach (var customer in _customersInLine)
        {
            var destination = customer.PositionInLine - ShiftForNextPosition;
            customer.SetDestination(destination);
        }
    }
}