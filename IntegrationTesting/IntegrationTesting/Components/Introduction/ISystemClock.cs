using System;

namespace IntegrationTesting.Components.Introduction
{
    public interface ISystemClock
    {
        DateTimeOffset Now();
    }
}