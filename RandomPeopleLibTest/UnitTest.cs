using NUnit.Framework;
using RandomPeopleLibrary.Example;
using RandomPeopleLibrary.Management;
using RandomPeopleLibrary.NPC;
using RandomPeopleLibrary.States;
using RandomPeopleLibrary.Structures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomPeopleLibTest
{
    public class Tests
    {
        private IArea<ITarget> emptyArea;
        
        private IArea<ITarget> simpleArea;

        private List<ITarget> exampleTargets;
        
        private List<ITarget> oneTargetList;

        private Random random;

        [SetUp]
        public void Setup()
        {

            oneTargetList = new List<ITarget>() { new Target(10, 10) };
            this.simpleArea = new Area(oneTargetList, random);
            this.random = new Random();

        }

        [Test]
        public async Task TestVerticalMove()
        {
            var unit = new Unit(simpleArea, random, 5, 10, 1);

            var status = await unit.ProcessNewFrame(1);
            status = await unit.ProcessNewFrame(1);
            Assert.True(status.state == UnitState.Moving && Math.Round(unit.ObjectPosition.PosX, 4) == 10 && Math.Round(unit.ObjectPosition.PosY, 4) == 6);
        }

        [Test]
        public async Task TestHorizontalMove()
        {
            var unit = new Unit(simpleArea, random, 5, 1, 10);

            var status = await unit.ProcessNewFrame(1);
            status = await unit.ProcessNewFrame(1);
            Assert.True(status.state == UnitState.Moving && Math.Round(unit.ObjectPosition.PosX, 4) == 6 && Math.Round(unit.ObjectPosition.PosY, 4) == 10);
        }

        [Test]
        public async Task TestMoveBothAxis()
        {
            oneTargetList = new List<ITarget>() { new Target(30, 40) };
            this.simpleArea = new Area(oneTargetList, random);


            var unit = new Unit(simpleArea, random, 5, 0, 0);

            var status = await unit.ProcessNewFrame(1);
            status = await unit.ProcessNewFrame(1);
            Assert.True(status.state == UnitState.Moving && Math.Round(unit.ObjectPosition.PosX, 4) == 3 && Math.Round(unit.ObjectPosition.PosY, 4) == 4);
        }
    }
}