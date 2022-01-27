using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.NPC
{
    public interface IBasicUnit <T> where T : ITarget
    {
        IBasicArea<T> GetArea();

        UnitState State { get; set; }

        UnitStatus GetState();

        Task<bool> Move(double timePassed);

        /// <summary>
        /// Method that will be called each frame Unit is spending it's time at target
        /// </summary>
        void OnSpendingTime(double timePassed);

        /// <summary>
        /// Method that will be called when Unit is arriving at target
        /// </summary>
        void OnArrival();

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
