// LC1279 - Traffic Light Controlled Intersection
// An intersection has two roads (road 1 and road 2) and a traffic light.
// Cars arrive and call carArrived(carId, roadId, direction, turnGreen, crossCar).
// Only one road may have a green light at a time.
// Cars on the same road do NOT need to stop for each other.

/*
 
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1279_TrafficLight
    {
        private int  _greenRoad = 1;        // road currently having green light
        private readonly object _lock = new object();

        public LC1279_TrafficLight() { }

        public void CarArrived(
            int    carId,         // car identifier
            int    roadId,        // road the car is on (1 or 2)
            int    direction,     // direction the car is going (irrelevant for logic)
            Action turnGreen,     // call to switch light to green for roadId
            Action crossCar)      // call to let the car cross
        {
            lock (_lock)
            {
                if (_greenRoad != roadId)
                {
                    _greenRoad = roadId;
                    turnGreen();
                }
                crossCar();
            }
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1279_Tests
    {
        // Helper: simulate a sequence of car arrivals and record events
        private static (List<string> events, bool conflict) Simulate(
            (int carId, int roadId, int direction)[] arrivals)
        {
            var light   = new LC1279_TrafficLight();
            var events  = new ConcurrentQueue<string>();
            bool conflict = false;
            int  crossing = 0; // number of cars currently crossing

            var tasks = new Task[arrivals.Length];
            for (int i = 0; i < arrivals.Length; i++)
            {
                var (carId, roadId, direction) = arrivals[i];
                tasks[i] = Task.Run(() =>
                {
                    light.CarArrived(carId, roadId, direction,
                        () => events.Enqueue($"green-road{roadId}"),
                        () =>
                        {
                            // Verify no car from the OTHER road crosses simultaneously
                            // (simplified: just record the crossing)
                            events.Enqueue($"cross-car{carId}-road{roadId}");
                        });
                });
            }

            Task.WaitAll(tasks);
            return (new List<string>(events), conflict);
        }

        [Fact]
        public void SameRoad_NeverTurnsGreenTwice()
        {
            // All cars on road 1 – light should only go green once
            var arrivals = new[]
            {
                (1, 1, 2), (2, 1, 1), (3, 1, 2)
            };

            var (events, _) = Simulate(arrivals);

            int greenCount = 0;
            foreach (var e in events) if (e.StartsWith("green-road1")) greenCount++;

            // May go green 0 times (started green) or 1 time, never 2+
            Assert.True(greenCount <= 1, $"Green switched too many times: {greenCount}");
        }

        [Fact]
        public void AllCars_EventuallyCross()
        {
            var arrivals = new[]
            {
                (1, 1, 2), (2, 1, 1),
                (3, 2, 1), (4, 2, 2),
                (5, 1, 2)
            };

            var (events, _) = Simulate(arrivals);

            for (int id = 1; id <= 5; id++)
                Assert.Contains(events, e => e.Contains($"cross-car{id}-"));
        }

        [Fact]
        public void RoadSwitching_TurnsGreenBeforeCrossing()
        {
            // Road 1 arrives first, then road 2 – green must appear before crossing on road 2
            var light    = new LC1279_TrafficLight();
            var log      = new List<string>();
            var lk       = new object();

            // Road 1 crosses first (starts green)
            light.CarArrived(1, 1, 2,
                () => { lock (lk) log.Add("green-1"); },
                () => { lock (lk) log.Add("cross-1"); });

            // Road 2 now: light must switch green
            light.CarArrived(2, 2, 1,
                () => { lock (lk) log.Add("green-2"); },
                () => { lock (lk) log.Add("cross-2"); });

            // "green-2" must appear before "cross-2"
            int greenIdx = log.IndexOf("green-2");
            int crossIdx = log.IndexOf("cross-2");

            Assert.True(greenIdx >= 0 && crossIdx >= 0);
            Assert.True(greenIdx < crossIdx, "Light must turn green before car crosses");
        }

        [Fact]
        public void HighConcurrency_NoCarsConflict()
        {
            // 20 cars on alternating roads – validate that the light is always
            // set to the correct road when crossCar is called.
            var light     = new LC1279_TrafficLight();
            var conflicts = new ConcurrentBag<string>();
            int greenRoad = 1; // track what road is currently green
            var lk        = new object();

            var arrivals = new List<(int, int, int)>();
            for (int i = 1; i <= 20; i++)
                arrivals.Add((i, i % 2 == 0 ? 2 : 1, 1));

            var tasks = new Task[arrivals.Count];
            for (int i = 0; i < arrivals.Count; i++)
            {
                var (carId, roadId, dir) = arrivals[i];
                tasks[i] = Task.Run(() =>
                {
                    light.CarArrived(carId, roadId, dir,
                        () => { lock (lk) greenRoad = roadId; },
                        () =>
                        {
                            lock (lk)
                            {
                                if (greenRoad != roadId)
                                    conflicts.Add($"Car {carId} on road {roadId} crossed with green on road {greenRoad}");
                            }
                        });
                });
            }

            Task.WaitAll(tasks);
            Assert.Empty(conflicts);
        }
    }
}
