using RandomPeopleLibrary.NPC.Needs;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.NPC
{
    public static class UnitExtensions
    {
        private static Random random = new Random();

        public static void Leave<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            unit.GetState().state = UnitState.Moving;
            unit.OnLeaving();
        }

        public static void FinishTask<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            unit.GetState().state = UnitState.Thinking;
            unit.OnFinishingTask();
        }

        public static void FindNewTarget<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            unit.GetState().target = unit.Area.PointsOfInterest[random.Next(0, unit.Area.PointsOfInterest.Count)];
            
            unit.GetState().state = UnitState.Leaving;
            unit.OnFindingNewTarget();
        }

        public static bool SpendTime<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit, float timePassed) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
        {   
            var timeLeft = unit.GetState().TimeLeft;
            unit.GetState().TimeLeft = timeLeft - timePassed;
            
            if (unit.GetState().target is NeedSatisfier && unit.IsTargetFullfillingNeeds((NeedSatisfier)unit.GetState().target))
            {
                unit.SatisfyNeed(timePassed, (NeedSatisfier)unit.GetState().target);   
            }
            
            unit.OnSpendingTime(timePassed);


            return timeLeft <= 0;   
        }

        public static void SatisfyNeed<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit, float timePassed, NeedSatisfier needSatisfier) 
            where Target : ITarget 
            where Need : Enum 
            where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            Need need = needSatisfier.SatisfiedNeed();
            unit.GetState().Needs[need] = unit.GetState().Needs[need] + timePassed * needSatisfier.GetSatisfactionRate();
        }


        public static bool IsTargetFullfillingNeeds<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit, NeedSatisfier satisfier) 
            where Target : ITarget 
            where Need : Enum 
            where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            return unit.GetState().Needs?.Keys.Contains(satisfier.SatisfiedNeed()) ?? false;
        }

        public static void Arrive<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            unit.GetState().TimeLeft = unit.GetState().target.GetActivityTime();
            unit.GetState().state = UnitState.Busy;
            unit.OnArrival(unit.GetState().target);
        }

        public static async Task ChangeState<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit, float timepassed) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            await unit.ProcessUnitFrame(timepassed);
        }

        public static async Task<UnitStatus<Target, Need>> ProcessNewFrame<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit, float timePassed) 
            where Target : ITarget 
            where Need : Enum 
            where NeedSatisfier : INeedSatisfier<Need>, Target
        {
            await unit.ChangeState(timePassed);

            return unit.GetState();
        }

        public static async Task ProcessUnitFrame<Target, NeedSatisfier, Need>(this IUnit<Target, NeedSatisfier, Need> unit, float timePassed) where Target : ITarget where Need : Enum where NeedSatisfier : INeedSatisfier<Need>, Target
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
