using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class StartupSettings : IStartupSettings
    {
        public Type StartingMenu { get; }
    }



    public interface IStartupSettings
    {
        public Type StartingMenu { get; }
    }
}