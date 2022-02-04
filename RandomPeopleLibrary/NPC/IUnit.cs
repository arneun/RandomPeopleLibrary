using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;

namespace RandomPeopleLibrary.NPC
{
    public interface IUnit<T> where T : ITarget
    {
        IArea<T> Area { get; }

        UnitStatus<T> GetState();

        bool Move(double timePassed);

        /// <summary>
        /// Method that will be called each frame Unit is spending it's time at target
        /// </summary>
        void OnSpendingTime(double timePassed);

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
