using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.NPC
{
    public static class UnitExtensions
    {
        private static Random random = new Random();

        public static void Leave<T>(this IUnit<T> unit) where T : ITarget
        {
            unit.GetState().state = UnitState.Moving;
            unit.OnLeaving();
        }

        public static void FinishTask<T>(this IUnit<T> unit) where T : ITarget
        {
            unit.GetState().state = UnitState.Thinking;
            unit.OnFinishingTask();
        }

        public static void FindNewTarget<T>(this IUnit<T> unit) where T : ITarget
        {
            unit.GetState().target = unit.Area.PointsOfInterest[random.Next(0, unit.Area.PointsOfInterest.Count )];
            
            unit.GetState().state = UnitState.Leaving;
            unit.OnFindingNewTarget();
        }

        public static bool SpendTime<T>(this IUnit<T> unit, double timePassed) where T : ITarget
        {   
            var timeLeft = unit.GetState().TimeLeft;
            unit.GetState().TimeLeft = timeLeft - timePassed;
            unit.OnSpendingTime(timePassed);
            return timeLeft <= 0;   
        }

        public static void Arrive<T>(this IUnit<T> unit) where T : ITarget
        {
            unit.GetState().TimeLeft = unit.GetState().target.GetActivityTime();
            unit.GetState().state = UnitState.Busy;
            unit.OnArrival(unit.GetState().target);
        }

        public static async Task ChangeState<T>(this IUnit<T> unit, double timepassed) where T : ITarget
        {
            await unit.ProcessUnitFrame(timepassed);
        }

        public static async Task<UnitStatus<T>> ProcessNewFrame<T>(this IUnit<T> unit, double timePassed) where T : ITarget
        {
            await unit.ChangeState(timePassed);

            return unit.GetState();
        }

        public static async Task ProcessUnitFrame<T>(this IUnit<T> unit, double timePassed) where T : ITarget
        {
            await Task.Run(() =>
            {
                switch (unit.GetState().state)
                {
                    case UnitState.Moving:
                        //  Console.WriteLine("Moving");
                        if (unit.Move(timePassed))
                        {
                            unit.Arrive();
                        }
                        break;
                    case UnitState.Busy:
                        //   Console.WriteLine("Busy");
                        if (unit.SpendTime(timePassed))
                        {
                            unit.FinishTask();
                        }
                        break;
                    case UnitState.Thinking:
                        unit.FindNewTarget();
                        //   Console.WriteLine("found new target");
                        break;
                    case UnitState.Leaving:
                        //     Console.WriteLine("Leaving");
                        unit.Leave();
                        unit.Move(timePassed);
                        break;
                }
            });
        }
    }
}
