using System;
using NUnit.Framework;
using static Features.Gameplay.Tests.Mothers.CoordinateMother;
using static Features.Gameplay.Tests.Mothers.ValueObjects.MapNodeMother;

namespace Features.Gameplay.Tests.Editor
{
    [TestFixture]
    public class MapNodeTests
    {
        [Test]
        public void ReturnRightCostTo()
        {
            //Given
            var actualNode = AMapNode(withCoordinate: ACoordinate(0,0), withWeight: 1);
            var neighbourNode = AMapNode(withCoordinate: ACoordinate(1, 0), withWeight: 5);
            var expectedCost = 5;
            
            //When
            var costTo = actualNode.CostTo(neighbourNode);
            //Then

            Assert.AreEqual(costTo, expectedCost);
        }
        
        [TestCase(0, 0, 1,0, 1, 5, 1 )]
        [TestCase(0, 0, 5,0, 1, 5, 5 )]
        [TestCase(0, 0, 1,1, 1, 5, 0.90f)]
        [TestCase(0, 0, 3,3, 1, 5, 3.36f )]
        public void ReturnRightEstimatedCost(
            int actualCoordinateX,
            int actualCoordinateY,
            int goalCoordinateX,
            int goalCoordinateY,
            int actualWeight,
            int GoalWeight,
            float expectedCost
        ) {
            //Given
            var actualNode = AMapNode(
                withCoordinate: ACoordinate(actualCoordinateX, actualCoordinateY), 
                withWeight: actualWeight
            );
            var neighbourNode = AMapNode(
                withCoordinate: ACoordinate(goalCoordinateX, goalCoordinateY), 
                withWeight: GoalWeight
            );
            var expected = expectedCost;
            
            //When
            var costTo = actualNode.EstimatedCostTo(neighbourNode);
            //Then

            Assert.AreEqual(Math.Round(costTo,2, MidpointRounding.AwayFromZero),
                Math.Round(expected,2,MidpointRounding.AwayFromZero));
        }
    }
}