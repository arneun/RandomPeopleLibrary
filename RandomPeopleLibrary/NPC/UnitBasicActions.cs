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

        public static async Task Leave(this IBasicUnit<ITarget> unit)
        {
            await Task.Run(() =>
                {
                    unit.GetState().state = UnitState.Moving;
                    unit.OnLeaving();
                });
        }

        public static async Task FinishTask(this IBasicUnit<ITarget> unit)
        {
            await Task.Run(() => {
                unit.GetState().state = UnitState.Thinking;
                unit.OnFinishingTask();
            });
        }

        public static async Task FindNewTarget(this IBasicUnit<ITarget> unit) 
        {
            var findingTargetTask = Task.Run(
                    () => unit.GetArea().PointsOfInterest[random.Next(0, unit.GetArea().PointsOfInterest.Count - 1)]);

            var status = Task.Run(() =>
            {
                unit.GetState().state = UnitState.Leaving;
                unit.OnFindingNewTarget();
            });

            await findingTargetTask;
            await status;
        }

        public static async Task<bool> SpendTime(this IBasicUnit<ITarget> unit, double timePassed)
        {
            return await Task.Run(() =>
            {
                var timeLeft = unit.GetState().TimeLeft;
                unit.GetState().TimeLeft = timeLeft - timePassed;
                unit.OnSpendingTime(timePassed);
                return timeLeft <= 0;
            });
        }

        public static async Task Arrive(this IBasicUnit<ITarget> unit)
        {
            await Task.Run(() =>
            {
                unit.GetState().TimeLeft = 0d;
                unit.GetState().state = UnitState.Busy;
                unit.OnArrival();
            });
        }

        public static async Task ChangeState(this IBasicUnit<ITarget> unit, double timepassed) 
        {
            await unit.ProcessUnitFrame(timepassed);
        }

        public static async Task<UnitStatus> ProcessNewFrame(this IBasicUnit<ITarget> unit, double timePassed) 
        {
            await unit.ChangeState(timePassed);

            return unit.GetState();
        }

        public static async Task ProcessUnitFrame(this IBasicUnit<ITarget> unit, double timePassed)
        {
            switch (unit.GetState().state)
            {
                case UnitState.Moving:
                    //  Console.WriteLine("Moving");
                    if (await unit.Move(timePassed))
                    {
                        await unit.Arrive();
                    }
                    break;
                case UnitState.Busy:
                    //   Console.WriteLine("Busy");
                    if (await unit.SpendTime(timePassed))
                    {
                        await unit.FinishTask();
                    }
                    break;
                case UnitState.Thinking:
                    await unit.FindNewTarget();
                    //   Console.WriteLine("found new target");
                    break;
                case UnitState.Leaving:
                    //     Console.WriteLine("Leaving");
                    await unit.Leave();
                    await unit.Move(timePassed);
                    break;
            }
        }
    }
}
