﻿using SqreenDispatcher.Services.Model;

namespace SqreenDispatcher.Services
{
    public interface ITarget
    {
        public void Notify(Message message);
    }
}