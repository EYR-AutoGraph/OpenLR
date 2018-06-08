using System;

// import File
using System.IO;

// import RouterDb
using Itinero;
// import method RouterDb.LoadOsmData
using Itinero.IO.Osm;
// import Vehicle
using Itinero.Osm.Vehicles;

// import Coder
using OpenLR;
// import OsmCoderProfile
using OpenLR.Osm;
// import ReferencedLine
using OpenLR.Referenced.Locations;

namespace my_openlr_tool
{
    class Program
    {
	static void Main(string[] args)
	{
	    Console.WriteLine("Hello World!");

	    Console.Write("Loading Luxembourg ... ");
	    var routerDb = new RouterDb();
	    using (var sourceStream =
	           File.OpenRead(
	    	   Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
	    			"../../../luxembourg-latest.osm.pbf")))
	    {
	        routerDb.LoadOsmData(sourceStream, Vehicle.Car);
	    }
	    Console.WriteLine("done");
	    // create coder.
	    var coder = new Coder(routerDb, new OsmCoderProfile());
	    
	    Console.Write("Building a line location, and encoding it ... ");
	    // build a line location from a shortest path.
	    var line = coder.BuildLine(
	        new Itinero.LocalGeo.Coordinate(
	    	49.67218282319583f, 6.142280101776122f),
	        new Itinero.LocalGeo.Coordinate(
	    	49.67776489459803f, 6.1342549324035645f));
	    
	    // encode this location.
	    var encoded = coder.Encode(line);
	    
	    // decode this location.
	    var decodedLine = coder.Decode(encoded) as ReferencedLine;
	    Console.WriteLine("done");
	}
    }
}
