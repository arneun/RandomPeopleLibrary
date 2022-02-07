using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.NPC.Needs;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;

namespace RandomPeopleLibrary.NPC
{
    public interface IUnit<Target, NeedsSatisfier, Need> 
        where Target : ITarget 
        where Need : Enum 
        where NeedsSatisfier : INeedSatisfier<Need> 
    {
        IArea<Target, NeedsSatisfier, Need> Area { get; }

        UnitStatus<Target, Need> GetState();

        bool Move(float timePassed);

        /// <summary>
        /// Method that will be called each frame Unit is spending it's time at target
        /// </summary>
        void OnSpendingTime(float timePassed);

        /// <summary>
        /// Method that will be called when Unit is arriving at target
        /// </summary>
        void OnArrival(ITarget arrivalTarget);

        /// <summary>
        /// Method that will be called each time Unit has found new target
        /// </summary>
        void OnFindingNewTarget();

        /// <summary>
        /// Method that will be called each time Unit is finishing task at target
        /// </summary>
        void OnFinishingTask();


        /// <summary>
        /// Method that will be called each time that Unit is leaving for next target
        /// </summary>
        void OnLeaving();
    }
}
