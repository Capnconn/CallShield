﻿using CallShield.UI.Models;

namespace CallShield.UI.Messages
{
    public class CallReceivedMessage(CallDetails callDetails)
    {
        public CallDetails CallDetails { get; } = callDetails;
    }
}
