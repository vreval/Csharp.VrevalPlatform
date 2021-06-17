using System.Collections.Generic;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class Wayfinding : Template
    {
        public List<WayfindingWaypoint> Waypoints;
        public List<WayfindingWaypoint> Destinations;
        public bool MultipleDestinations;
        public bool VisitInOrder;
    }
}