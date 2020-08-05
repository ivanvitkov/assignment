﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Logger
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string message);

    }
}
