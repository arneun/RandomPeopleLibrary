using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.NPC
{
    public static class UnitExtensions
    {
        private static Random random = new Random();

        public static void Leave(this IUnit unit)
        {
            unit.GetState().state = UnitState.Moving;
            unit.OnLeaving();
        }

        public static void FinishTask(this IUnit unit)
        {
            unit.GetState().state = UnitState.Thinking;
            unit.OnFinishingTask();
        }

        public static void FindNewTarget(this IUnit unit) 
        {
            
            unit.GetState().target = unit.Area.PointsOfInterest[random.Next(0, unit.Area.PointsOfInterest.Count - 1)];

            unit.GetState().state = UnitState.Leaving;
            unit.OnFindingNewTarget();
        }

        public static bool SpendTime(this IUnit unit, double timePassed)
        {   
            var timeLeft = unit.GetState().TimeLeft;
            unit.GetState().TimeLeft = timeLeft - timePassed;
            unit.OnSpendingTime(timePassed);
            return timeLeft <= 0;   
        }

        public static void Arrive(this IUnit unit)
        {
            unit.GetState().TimeLeft = 0d;
            unit.GetState().state = UnitState.Busy;
            unit.OnArrival();
        }

        public static async Task ChangeState(this IUnit unit, double timepassed) 
        {
            await unit.ProcessUnitFrame(timepassed);
        }

        public static async Task<UnitStatus> ProcessNewFrame(this IUnit unit, double timePassed) 
        {
            await unit.ChangeState(timePassed);

            return unit.GetState();
        }

        public static async Task ProcessUnitFrame(this IUnit unit, double timePassed)
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
        }
    }
}
