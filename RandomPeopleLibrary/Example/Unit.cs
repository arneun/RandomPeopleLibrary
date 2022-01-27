using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.NPC;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RandomPeopleLibrary.Example
{
    /// <summary>
    /// Example implementation of IUnit interface
    /// </summary>
    public class Unit : IExampleUnit, IMapObject
    {
        private readonly IExampleArea area;

        public IExampleArea GetArea()
        {
            return area;
        }

        private Position position;

        private ExampleUnitStatus status;

        private readonly double speed;

        public Unit(IExampleArea placeToLive, double speed)
        {
            this.area = placeToLive;
            this.speed = speed;
            this.status = new ExampleUnitStatus
            {
                state = UnitState.Thinking
            };
        }

        public Unit(IExampleArea placeToLive, Random random, double speed, double posX, double posY)
        {
            this.area = placeToLive;
            this.speed = speed;
            position = new Position(posX, posY);
            this.status = new ExampleUnitStatus
            {
                state = UnitState.Thinking
            };
        }

        public Position ObjectPosition => position;

        public UnitState State { get => this.status.state; set => this.status.state = value; }

        public UnitStatus GetState()
        {
            return this.status;
        }

        public async Task<bool> Move(double timePassed) 
        {
            var velocity = speed * timePassed;

            var dirX = (this.status.target.ObjectPosition.PosX - this.ObjectPosition.PosX);


            var dirY = (this.status.target.ObjectPosition.PosY - this.position.PosY);
            var rotation = Math.Atan2(dirY, dirX); 
            

            var moveEnd = new Position(velocity * Math.Cos(rotation) + this.position.PosX, this.position.PosY + velocity * Math.Sin(rotation));

            /// need to check for arrival
            var distanceTarget = await this.getDistanceSquared(this.position, this.status.target.ObjectPosition);
            var distanceMoveEnd = await this.getDistanceSquared(this.position, moveEnd);

           // Console.WriteLine($"Comparing distance, to target {distanceTarget}, to move end {distanceMoveEnd}");
            if (distanceTarget >= distanceMoveEnd)
            {
                this.position = moveEnd;
                return false;
            }
            else
            {
             //   Console.WriteLine("arrived");
                this.position = this.status.target.ObjectPosition;
                
                return true;
            }
        }

        private async Task<double> getDistanceSquared(Position start, Position end) 
        {
            return await Task.Run(() => 
            {
                return (end.PosX - start.PosX) * (end.PosX - start.PosX)
                    + (end.PosY - start.PosY) * (end.PosY - start.PosY);
            });
        }

        /// <summary>
        /// Method that will be called when Unit is arriving at target
        /// </summary>
        public void OnArrival() 
        {
        }

        /// <summary>
        /// Method that will be called each frame Unit is spending it's time at target
        /// </summary>
        public void OnSpendingTime(double timePassed) 
        {
            
        }

        /// <summary>
        /// Method that will be called each time Unit has found new target
        /// </summary>
        public void OnFindingNewTarget() 
        {
            
            
        }

        /// <summary>
        /// Method that will be called each time Unit is finishing task at target
        /// </summary>
        public void OnFinishingTask() 
        {
        }

        /// <summary>
        /// Method that will be called each time that Unit is leaving for next target
        /// </summary>
        public void OnLeaving() 
        {
            
        }
    }
}
