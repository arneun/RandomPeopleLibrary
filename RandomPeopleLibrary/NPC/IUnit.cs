﻿using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.NPC
{
    public interface IUnit : IBasicUnit<ITarget>
    {
        new IArea GetArea();
    }
}